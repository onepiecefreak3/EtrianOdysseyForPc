using EtrianOdysseyPc.Events;
using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Managers;
using EtrianOdysseyPc.Models;
using Komponent.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc.Providers
{
    public class LabyrinthProvider
    {
        public Direction LookDirection { get; set; }
        public Cell CurrentCell { get; set; }
        private Cell[] _cells;

        private byte _entryX;
        private byte _entryY;

        private Viewport3D _labView;
        private PerspectiveCamera _camera;

        public int TurnSpeed { get; set; } = 500;
        public int MoveSpeed { get; set; } = 500;

        public LabyrinthProvider(string labFile, Viewport3D labView)
        {
            if (!File.Exists(labFile))
                throw new FileNotFoundException(labFile);

            _labView = labView;

            LoadLabyrinth(labFile);

            SetupView();
        }

        private void SetupView()
        {
            Vector3D lookDir = GetLookDirectionVector();

            _camera = new PerspectiveCamera(GetTilePoint(_entryX, _entryY) + new Vector3D(5, 1, 5), lookDir, new Vector3D(0, 1, 0), 60);
            _labView.Camera = _camera;

            var modelVisual = new ModelVisual3D();
            modelVisual.Content = new AmbientLight(Colors.White);
            _labView.Children.Add(modelVisual);
        }

        private Point3D GetTilePoint(byte x, byte y)
        {
            return new Point3D(x * -10, 0, y * 10);
        }

        private Vector3D GetLookDirectionVector()
        {
            switch (LookDirection)
            {
                case Direction.North:
                    return new Vector3D(0, 0, -1);
                case Direction.East:
                    return new Vector3D(1, 0, 0);
                case Direction.South:
                    return new Vector3D(0, 0, 1);
                case Direction.West:
                    return new Vector3D(-1, 0, 0);
                default:
                    throw new InvalidOperationException($"Unknown direction {LookDirection}");
            }
        }

        private void LoadLabyrinth(string file)
        {
            using (var br = new BinaryReaderX(File.OpenRead(file)))
            {
                var labFile = br.ReadStruct<EOL>();
                _entryX = labFile.eolHeader.entryX;
                _entryY = labFile.eolHeader.entryY;
                LookDirection = (Direction)labFile.eolHeader.entryLookDirection;

                // Read events
                var events = new Dictionary<int, IEvent>();
                var eventInfos = new Dictionary<int, EventInfo>();
                for (int i = 0; i < labFile.evtHeader.eventCount; i++)
                {
                    var evtInfo = br.ReadStruct<EventInfo>();
                    eventInfos.Add(evtInfo.eventId, evtInfo);

                    switch (evtInfo.type)
                    {
                        case EventType.Text:
                            var textLength = br.ReadInt32();
                            var text = br.ReadString(textLength, Encoding.UTF8);

                            events.Add(evtInfo.eventId, new TextEvent(text, evtInfo.activateCond, null));
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown event type {evtInfo.type}");
                    }
                }

                // Relate events
                foreach (var evtInfo in eventInfos)
                {
                    if (evtInfo.Value.parentId > -1)
                        if (!events.ContainsKey(evtInfo.Value.parentId))
                            throw new InvalidOperationException($"Unknown parent event with id {evtInfo.Value.parentId}");
                        else
                            events[evtInfo.Value.parentId].ChildEvent = events[evtInfo.Value.eventId];
                }

                // Create cells array
                _cells = new Cell[labFile.cellHeader.cellCount];
                for (int i = 0; i < _cells.Length; i++)
                {
                    var cellInfo = labFile.cellBody.cells[i];

                    _cells[i] = new Cell(cellInfo.x, cellInfo.y, cellInfo.isSolid);
                    _cells[i].Events = eventInfos.
                        Where(x => x.Value.x == cellInfo.x && x.Value.y == cellInfo.y && x.Value.parentId < 0).
                        Select(x => events[x.Key]).
                        ToArray();
                }

                // Add models to viewport
                foreach (var cellInfo in labFile.cellBody.cells)
                    foreach (var modelInfo in cellInfo.modelPlacements)
                    {
                        var modelName = labFile.modBody.models.First(x => x.modelId == modelInfo.modelId).path;
                        var eom = new EOM(Path.Combine("Ressources", "Models", modelName));

                        var geometry = eom.RetrieveModel();
                        var tilePoint = GetTilePoint(cellInfo.x, cellInfo.y);
                        geometry.Transform = new TranslateTransform3D(new Vector3D(modelInfo.x + tilePoint.X, modelInfo.y, modelInfo.z + tilePoint.Z));

                        var modelVisual = new ModelVisual3D();
                        modelVisual.Content = geometry;
                        _labView.Children.Add(modelVisual);
                    }
            }
        }

        public void Enter()
        {
            Enter(_entryX, _entryY);
        }

        public void Enter(byte x, byte y)
        {
            CurrentCell = _cells.First(c => c.X == x && c.Y == y);
        }

        #region Move
        public void Move(MoveDirection direct)
        {
            switch (LookDirection)
            {
                case Direction.North:
                    switch (direct)
                    {
                        case MoveDirection.Backward:
                            MoveInternal(CurrentCell.X, CurrentCell.Y + 1);
                            break;
                        case MoveDirection.Forward:
                            MoveInternal(CurrentCell.X, CurrentCell.Y - 1);
                            break;
                        case MoveDirection.Left:
                            MoveInternal(CurrentCell.X + 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Right:
                            MoveInternal(CurrentCell.X - 1, CurrentCell.Y);
                            break;
                    }
                    break;
                case Direction.South:
                    switch (direct)
                    {
                        case MoveDirection.Backward:
                            MoveInternal(CurrentCell.X, CurrentCell.Y - 1);
                            break;
                        case MoveDirection.Forward:
                            MoveInternal(CurrentCell.X, CurrentCell.Y + 1);
                            break;
                        case MoveDirection.Left:
                            MoveInternal(CurrentCell.X - 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Right:
                            MoveInternal(CurrentCell.X + 1, CurrentCell.Y);
                            break;
                    }
                    break;
                case Direction.East:
                    switch (direct)
                    {
                        case MoveDirection.Backward:
                            MoveInternal(CurrentCell.X + 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Forward:
                            MoveInternal(CurrentCell.X - 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Left:
                            MoveInternal(CurrentCell.X, CurrentCell.Y - 1);
                            break;
                        case MoveDirection.Right:
                            MoveInternal(CurrentCell.X, CurrentCell.Y + 1);
                            break;
                    }
                    break;
                case Direction.West:
                    switch (direct)
                    {
                        case MoveDirection.Backward:
                            MoveInternal(CurrentCell.X - 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Forward:
                            MoveInternal(CurrentCell.X + 1, CurrentCell.Y);
                            break;
                        case MoveDirection.Left:
                            MoveInternal(CurrentCell.X, CurrentCell.Y + 1);
                            break;
                        case MoveDirection.Right:
                            MoveInternal(CurrentCell.X, CurrentCell.Y - 1);
                            break;
                    }
                    break;
            }
        }

        private void MoveInternal(int newx, int newy)
        {
            if (!_moveFinished || !_turnFinished)
                return;

            var newCell = _cells.FirstOrDefault(x => x.X == newx && x.Y == newy);
            if (newCell == null) return;

            // Check if cell is solid
            if (newCell.IsSolid) return;

            _moveFinished = false;

            // Move to new cell
            CurrentCell = newCell;

            // Move camera
            MoveCamera();
        }

        private void MoveCamera()
        {
            var anim = new Point3DAnimation(GetTilePoint(CurrentCell.X, CurrentCell.Y) + new Vector3D(5, 1, 5), new Duration(TimeSpan.FromMilliseconds(MoveSpeed)));
            anim.Completed += Anim_Completed1;

            _camera.BeginAnimation(PerspectiveCamera.PositionProperty, anim);
        }

        private void Anim_Completed1(object sender, EventArgs e)
        {
            _moveFinished = true;
        }

        private bool _moveFinished = true;
        #endregion

        #region Turn
        public void Turn(TurnDirection turnDirect)
        {
            if (!_turnFinished || !_moveFinished)
                return;

            _turnFinished = false;

            var newDirect = (int)LookDirection + (int)turnDirect;
            if (newDirect < 0)
                newDirect = 3;
            else if (newDirect > 3)
                newDirect = 0;

            LookDirection = (Direction)newDirect;

            TurnCamera();
        }

        private void TurnCamera()
        {
            var anim = new Vector3DAnimation(GetLookDirectionVector(), new Duration(TimeSpan.FromMilliseconds(TurnSpeed)));
            anim.Completed += Anim_Completed;

            _camera.BeginAnimation(PerspectiveCamera.LookDirectionProperty, anim);
        }

        private void Anim_Completed(object sender, EventArgs e)
        {
            _turnFinished = true;
        }

        private bool _turnFinished = true;
        #endregion

        public class Cell
        {
            public bool IsSolid { get; }

            public byte X { get; }
            public byte Y { get; }

            public Cell(byte x, byte y, bool isSolid)
            {
                X = x;
                Y = y;
                IsSolid = isSolid;
            }

            public IEvent[] Events { get; set; }
        }
    }

    public enum Direction : int
    {
        North,
        East,
        South,
        West
    }

    public enum MoveDirection : int
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public enum TurnDirection : int
    {
        Left = -1,
        Right = 1
    }
}
