using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Interfaces
{
    internal interface IEvent
    {
        ActivateCondition ActivateCondition { get; }
        string OnFlag { get; }

        IEvent ChildEvent { get; set; }

        void Execute(IDataContext context);
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
}
