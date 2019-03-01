using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Events
{
    internal class TextEvent : IEvent
    {
        public ActivateCondition ActivateCondition { get; }
        public string OnFlag { get; }

        public IEvent ChildEvent { get; set; }

        private string _text;

        public TextEvent(string text, ActivateCondition cond, string flag)
        {
            ActivateCondition = cond;
            OnFlag = flag;

            _text = text;
        }

        public void Execute(IDataContext context)
        {
            // TODO
        }
    }
}
