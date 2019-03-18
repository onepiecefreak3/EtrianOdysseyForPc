using EtrianOdysseyPC2.Cameras;
using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.EventHandlers;
using EtrianOdysseyPC2.Interfaces;
using EtrianOdysseyPC2.Managers;
using EtrianOdysseyPC2.Models.Camera;
using HelixToolkit.Wpf.SharpDX;
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
    class LabyrinthElement : IUiElement
    {
        private LabyrinthManager _labManager;
        // private MapManager _mapManager;

        private Viewport3DX _viewport;
        private PlayerCamera _camera;

        public string Name => "Labyrinth";

        public ElementContext ElementContext { get; private set; }

        public event SwitchUiElementEventHandler SwitchUiElement;

        public LabyrinthElement(string labFile)
        {
            _labManager = new LabyrinthManager(labFile);

            SetupContext();
        }

        private void SetupContext()
        {
            ElementContext = new ElementContext();

            ElementContext.Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            ElementContext.Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            _viewport = new Viewport3DX();
            Grid.SetColumn(_viewport, 0);
            ElementContext.Grid.Children.Add(_viewport);

            var entryCoord = _labManager.EntryCoord;
            _camera = new PlayerCamera(entryCoord.X, entryCoord.Y, _labManager.EntryLookDirection, 10);

            _viewport.Camera = _camera;
        }

        // TODO: Catch key presses
        public void KeyPress(KeyEventArgs e)
        {
            if (e.Key == KeyBindingManager.Global.LabyrinthBindings.Forward)
            {

            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.Backward)
            {

            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.Left)
            {

            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.Right)
            {

            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.TurnLeft)
            {
                var newDir = _camera.CurrentLookDirection - 1;
                if (newDir < 0) newDir = (LookDirection)3;

                _camera.Turn(newDir);
            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.TurnRight)
            {
                var newDir = _camera.CurrentLookDirection + 1;
                if ((int)newDir > 3) newDir = 0;

                _camera.Turn(newDir);
            }
            else if (e.Key == KeyBindingManager.Global.LabyrinthBindings.Action)
            {

            }
        }
    }
}
