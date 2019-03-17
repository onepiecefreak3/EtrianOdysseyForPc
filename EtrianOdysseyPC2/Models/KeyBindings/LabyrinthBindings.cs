using EtrianOdysseyPC2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPC2.Models.KeyBindings
{
    class LabyrinthBindings
    {
        private KeyBindingManager _bindManager;

        public Key Forward => _bindManager.GetKeyBinding("lab_forward");
        public Key Backward => _bindManager.GetKeyBinding("lab_backward");
        public Key Right => _bindManager.GetKeyBinding("lab_right");
        public Key Left => _bindManager.GetKeyBinding("lab_left");
        public Key TurnRight => _bindManager.GetKeyBinding("lab_turnright");
        public Key TurnLeft => _bindManager.GetKeyBinding("lab_turnleft");
        public Key Action => _bindManager.GetKeyBinding("lab_action");

        public LabyrinthBindings(KeyBindingManager bindManager)
        {
            _bindManager = bindManager;
        }
    }
}
