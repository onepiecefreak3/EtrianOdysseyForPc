using EtrianOdysseyPc.Events;
using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Models;
using Komponent.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc.Providers
{
    public class LabyrinthProvider
    {
        private Direction _lookDirection;
        private PerspectiveCamera _camera;
        private Cell _currentCell;
        private Cell[] _cells;

        public LabyrinthProvider(string labFile)
        {
            if (!File.Exists(labFile))
                throw new FileNotFoundException(labFile);

            LoadLabyrinth(labFile);
        }

        private void LoadLabyrinth(string file)
        {
            // TODO: Load labyrinth
            using (var br = new BinaryReaderX(File.OpenRead(file)))
            {
                // Read first half
                var labFile = br.ReadStruct<EOL>();

                // Read event part
                var evts = new Dictionary<int, IEvent>();
                var evtCoords = new Dictionary<int, (byte x, byte y)>();
                for (int i = 0; i < labFile.evtHeader.eventCount; i++)
                {
                    var evtId = br.ReadInt32();
                    var x = br.ReadByte();
                    var y = br.ReadByte();

                    var cond = br.ReadStruct<ActivateCondition>();
                    var type = br.ReadStruct<EventType>();

                    var parentId = br.ReadInt32();
                    if (parentId <= -1)
                        evtCoords.Add(evtId, (x, y));

                    string flag;
                    if (cond == ActivateCondition.OnFlag)
                    {
                        var length = br.ReadByte();
                        flag = br.ReadString(length, Encoding.UTF8);
                    }

                    switch (type)
                    {
                        case EventType.Text:
                            var textLength = br.ReadInt32();
                            var text = br.ReadString(textLength, Encoding.UTF8);
                            evts.Add(evtId, new TextEvent(text, cond, null));
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown event type {type}");
                    }
                }

                // Create cells array
                _cells = new Cell[labFile.cells.cellCount];
                for (int i = 0; i < _cells.Length; i++)
                {
                    var cellInfo = labFile.cells.cellInfos[i];
                    _cells[i] = new Cell(cellInfo.x, cellInfo.y, cellInfo.isSolid);

                    var cellEvents = evtCoords.Where(x => x.Value.x == cellInfo.x && x.Value.y == cellInfo.y).ToList();
                    if(cellEvents.Any())
                    {
                        var finalEvts = new List<IEvent>();
                        foreach(var evt in cellEvents)
                        {
                            var localEvt = evts[evt.Key];
                            finalEvts.Add(localEvt);
                            while(evts.Any(x=>x.))
                        }
                    }
                }
            }
        }

        #region Move
        public void Move(Direction direct)
        {
            // TODO: check if tile solid
            // TODO: Move player coordinates
            // TODO: Move camera
        }

        private void MoveCamera()
        {
            // TODO: Move camera according to new location
        }
        #endregion

        #region Turn
        public void Turn(TurnDirection turnDirect)
        {
            var newDirect = (int)_lookDirection + (int)turnDirect;
            if (newDirect < 0)
                newDirect = 3;
            else if (newDirect > 3)
                newDirect = 0;

            _lookDirection = (Direction)newDirect;

            TurnCamera();
        }

        private void TurnCamera()
        {
            // TODO: animate turn
            switch (_lookDirection)
            {
                case Direction.North:
                    _camera.LookDirection = new Vector3D(0, 0, 1);
                    break;
                case Direction.East:
                    _camera.LookDirection = new Vector3D(1, 0, 0);
                    break;
                case Direction.South:
                    _camera.LookDirection = new Vector3D(0, 0, -1);
                    break;
                case Direction.West:
                    _camera.LookDirection = new Vector3D(-1, 0, 0);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown LookDirection {_lookDirection}");
            }
        }
        #endregion

        private class Cell
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

    public enum TurnDirection : int
    {
        Left = -1,
        Right = 1
    }
}
