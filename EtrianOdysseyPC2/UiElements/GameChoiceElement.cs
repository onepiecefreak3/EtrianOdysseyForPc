using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.EventHandlers;
using EtrianOdysseyPC2.Interfaces;
using EtrianOdysseyPC2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPC2.UiElements
{
    class GameChoiceElement : IUiElement
    {
        public string Name => "GameChoice";

        public ModelContext DataContext { get; }

        public event SwitchUiElementEventHandler SwitchUiElement;

        public GameChoiceElement()
        {
            // TODO: Load game zips from "games" directory
            // TODO: Setup game choice view
            // TODO: Setup game choice list
        }

        public void KeyPress(KeyEventArgs e)
        {
            // TODO: Remove debug load stub
            if (e.Key == KeyBindingManager.Global.MenuBindings.Confirm)
            {
                SwitchUiElement?.Invoke(this, new SwitchUiElementEventArgs(new TownElement()));
            }
        }
    }
}
