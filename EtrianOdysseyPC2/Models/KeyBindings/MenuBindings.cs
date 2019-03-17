using EtrianOdysseyPC2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPC2.Models.KeyBindings
{
    class MenuBindings
    {
        private KeyBindingManager _bindManager;

        public Key Up => _bindManager.GetKeyBinding("menu_up");
        public Key Down => _bindManager.GetKeyBinding("menu_down");
        public Key Left => _bindManager.GetKeyBinding("menu_left");
        public Key Right => _bindManager.GetKeyBinding("menu_right");
        public Key Confirm => _bindManager.GetKeyBinding("menu_confirm");
        public Key Cancel => _bindManager.GetKeyBinding("menu_cancel");

        public MenuBindings(KeyBindingManager bindManager)
        {
            _bindManager = bindManager;
        }
    }
}
