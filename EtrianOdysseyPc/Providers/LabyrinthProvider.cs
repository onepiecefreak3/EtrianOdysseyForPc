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
            // TODO: Turn camera according to new direction
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

    public enum ActivateCondition
    {
        Immediate = -1,     // should only be used for chained events
        OnFlag,
        OnButtonPress,
        OnButtonPressNorth,
        OnButtonPressEast,
        OnButtonPressSouth,
        OnButtonPressWest,
    }

    public enum EventType
    {
        Move,
        Turn,
        LookDirectionTransform,
        Text,
        Decision
    }
}
