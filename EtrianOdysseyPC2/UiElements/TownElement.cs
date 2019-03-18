using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.EventHandlers;
using EtrianOdysseyPC2.Interfaces;
using EtrianOdysseyPC2.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EtrianOdysseyPC2.UiElements
{
    class TownElement : IUiElement
    {
        private Canvas _canvas;

        public string Name => "Town";

        public ElementContext ElementContext { get; private set; }

        public event SwitchUiElementEventHandler SwitchUiElement;

        public TownElement()
        {
            ElementContext = new ElementContext();
            SetupContext();
        }

        private void SetupContext()
        {
            // Setup rows
            ElementContext.Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // Setup view
            var viewbox = new Viewbox();
            _canvas = new Canvas();

            viewbox.Child = _canvas;
            Grid.SetRow(viewbox, 0);
            ElementContext.Grid.Children.Add(viewbox);
        }

        public void KeyPress(KeyEventArgs e)
        {
            // TODO: Catch key presses
            if (e.Key == KeyBindingManager.Global.MenuBindings.Up)
            {

            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Down)
            {

            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Left)
            {

            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Right)
            {

            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Confirm)
            {

            }
            else if (e.Key == KeyBindingManager.Global.MenuBindings.Cancel)
            {

            }
        }
    }
}
