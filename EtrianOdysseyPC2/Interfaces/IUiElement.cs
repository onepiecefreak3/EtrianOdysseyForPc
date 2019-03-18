using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPC2.Interfaces
{
    interface IUiElement
    {
        string Name { get; }
        ElementContext ElementContext { get; }

        event SwitchUiElementEventHandler SwitchUiElement;

        void KeyPress(KeyEventArgs e);
    }
}
