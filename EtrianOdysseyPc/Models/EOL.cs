using Komponent.IO;
using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Models
{
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EOL
    {
        public EOLHeader eolHeader;

        public ModHeader modHeader;
        public ModBody modBody;

        public CellHeader cellHeader;
        public CellBody cellBody;

        public EventHeader evtHeader;
    }

    [DebuggerDisplay("EntryCoordinate: ({entryX},{entryY})")]
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class EOLHeader
    {
        [FixedLength(4)]
        public string labMagic;     // EOL0
        public byte entryX;
        public byte entryY;
        public byte entryLookDirection;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class ModHeader
    {
        [FixedLength(4)]
        public string modMagic;     // MOD0
        public int modelCount;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class ModBody
    {
        [VariableLength("modHeader.modelCount")]
        public ModInfo[] models;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class ModInfo
    {
        public short modelId;
        public byte pathLength;
        [VariableLength("pathLength")]
        public string path;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class CellHeader
    {
        [FixedLength(4)]
        public string cellMagic;    // CELL
        public int cellCount;
    }

    [DebuggerDisplay("Cells[{cells.Length}]")]
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class CellBody
    {
        [VariableLength("cellHeader.cellCount")]
        public CellInfo[] cells;
    }

    [DebuggerDisplay("Coordinate: ({x},{y})")]
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class CellInfo
    {
        public byte x;
        public byte y;
        public bool isSolid;
        public int modelCount;

        [VariableLength("modelCount")]
        public CellModelPlacement[] modelPlacements;
    }

    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class CellModelPlacement
    {
        public short modelId;
        // position relative to cell
        public float x;
        public float y;
        public float z;
    }

    [DebuggerDisplay("Events[{eventCount}]")]
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    [Alignment(16)]
    public class EventHeader
    {
        [FixedLength(4)]
        public string eventMagic;   // EVT0
        public int eventCount;
    }

    [DebuggerDisplay("{type}")]
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EventInfo
    {
        public int eventId;

        public byte x;
        public byte y;

        public ActivateCondition activateCond;
        public EventType type;

        public byte flagLength;
        [VariableLength("flagLength")]
        public string flagName;

        public int parentId;
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
