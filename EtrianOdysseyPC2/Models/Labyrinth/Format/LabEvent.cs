using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    [Alignment(0x4)]
    class LabEvent
    {
        public short eventId;
        public short cellId;

        public ActivateCondition cond;
        public EventType type;

        public LabString flag;
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
