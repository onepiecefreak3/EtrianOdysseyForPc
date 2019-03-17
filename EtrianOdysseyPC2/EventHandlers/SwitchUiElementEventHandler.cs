using EtrianOdysseyPC2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.EventHandlers
{
    delegate void SwitchUiElementEventHandler(object sender, SwitchUiElementEventArgs e);

    class SwitchUiElementEventArgs
    {
        public IUiElement UiElement { get; set; }

        public SwitchUiElementEventArgs(IUiElement element)
        {
            UiElement = element;
        }
    }
}
