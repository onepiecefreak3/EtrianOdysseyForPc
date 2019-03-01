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
        }

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

        private class Cell
        {
            public bool IsSolid { get; set; }

            public byte X { get; set; }
            public byte Y { get; set; }

            public Event[] Events { get; set; }
        }

        private class Event
        {
            public ActivateCondition ActivateCondition { get; set; }
            public EventType EventType { get; set; }
            public Event ChainedEvent { get; set; }
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
