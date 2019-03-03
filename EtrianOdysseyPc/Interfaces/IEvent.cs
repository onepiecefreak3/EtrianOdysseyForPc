using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Interfaces
{
    public interface IEvent
    {
        ActivateCondition ActivateCondition { get; }
        string OnFlag { get; }

        IEvent ChildEvent { get; set; }

        void Execute(IDataContext context);
    }
}
