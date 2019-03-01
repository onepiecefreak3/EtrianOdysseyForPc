using Komponent.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Models
{
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EOL
    {
        public EOLHeader eolHeader;
        public CellsBody cells;
        public EventHeader evtHeader;

        // Following are events
        //[VariableLength("eventCount")]
        //public EventInfo[] events;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EOLHeader
    {
        [FixedLength(4)]
        public string labMagic;     // EOL0
        public byte entryX;
        public byte entryY;
        public byte entryLookDirection;
        [FixedLength(9)]
        public byte[] reserved1;
    }

    public class CellsBody
    {
        [FixedLength(4)]
        public string cellMagic;    // CELL
        public int cellCount;
        [FixedLength(8)]
        public byte[] reserved2;
        [VariableLength("cellCount")]
        public CellInfo[] cellInfos;
    }

    public class EventHeader
    {
        [FixedLength(4)]
        public string eventMagic;   // EVT0
        public int eventCount;
        [FixedLength(8)]
        public byte[] reserved3;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class CellInfo
    {
        public byte x;
        public byte y;
        public bool isSolid;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EventInfo
    {
        public byte x;
        public byte y;
        public ActivateCondition cond;
    }

    public enum ActivateCondition : sbyte
    {
        Immediate = -1,     // should only be used for chained events
        OnFlag,
        OnButtonPress,
        OnButtonPressNorth,
        OnButtonPressEast,
        OnButtonPressSouth,
        OnButtonPressWest,
    }

    public enum EventType : byte
    {
        Move,
        Turn,
        LookDirectionTransform,
        Text,
        Decision
    }
}
