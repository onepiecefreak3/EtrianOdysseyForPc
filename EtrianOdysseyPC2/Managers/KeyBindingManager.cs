using EtrianOdysseyPC2.Models.KeyBindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPC2.Managers
{
    class KeyBindingManager
    {
        private static Lazy<KeyBindingManager> _lazy = new Lazy<KeyBindingManager>(() => new KeyBindingManager());
        public static KeyBindingManager Global => _lazy.Value;

        public MenuBindings MenuBindings { get; private set; }
        public LabyrinthBindings LabyrinthBindings { get; private set; }

        private Dictionary<string, Key> _keyBindings;

        public KeyBindingManager()
        {
            _keyBindings = new Dictionary<string, Key>();

            RegisterDefaultBindings();
            RegisterShortcuts();
        }

        private void RegisterDefaultBindings()
        {
            // TODO: Set keybindings based on saved preferences of user

            RegisterKeyBinding("menu_up", Key.Up);
            RegisterKeyBinding("menu_down", Key.Down);
            RegisterKeyBinding("menu_left", Key.Left);
            RegisterKeyBinding("menu_right", Key.Right);
            RegisterKeyBinding("menu_confirm", Key.Enter);
            RegisterKeyBinding("menu_cancel", Key.Return);

            RegisterKeyBinding("lab_forward", Key.Up);
            RegisterKeyBinding("lab_backward", Key.Down);
            RegisterKeyBinding("lab_left", Key.Q);
            RegisterKeyBinding("lab_right", Key.E);
            RegisterKeyBinding("lab_turnright", Key.Left);
            RegisterKeyBinding("lab_turnleft", Key.Right);
            RegisterKeyBinding("lab_action", Key.Space);
        }

        private void RegisterShortcuts()
        {
            MenuBindings = new MenuBindings(this);
            LabyrinthBindings = new LabyrinthBindings(this);
        }

        public void RegisterKeyBinding(string name, Key value)
        {
            _keyBindings.Add(name, value);
        }

        public Key GetKeyBinding(string name)
        {
            return _keyBindings[name];
        }

        public void SetKeyBinding(string name, Key value)
        {
            _keyBindings[name] = value;
        }
    }
}
