using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Models;
using EtrianOdysseyPc.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc.UIElements
{
    internal class LabyrinthElement : IUiElement
    {
        public IDataContext DataContext { get; }

        private TextBox _text;
        private Viewport3D _viewport;

        private LabyrinthProvider _labyrinth;

        public LabyrinthElement(string labFile)
        {
            DataContext = new ModelContext();
            SetupView();

            _labyrinth = new LabyrinthProvider(Path.Combine("Ressources", "Labyrinths", labFile), _viewport);
            _labyrinth.Enter();

            UpdateUi();
        }

        private void SetupView()
        {
            DataContext.WindowGrid.Background = Brushes.Wheat;
            DataContext.WindowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            DataContext.WindowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            _text = new TextBox();
            Grid.SetRow(_text, 0);
            DataContext.WindowGrid.Children.Add(_text);

            _viewport = new Viewport3D();
            Grid.SetRow(_viewport, 1);
            DataContext.WindowGrid.Children.Add(_viewport);
        }

        private void UpdateUi()
        {
            _text.Text = $"{_labyrinth.CurrentCell.X};{_labyrinth.CurrentCell.Y} {_labyrinth.LookDirection} {(_viewport.Camera as PerspectiveCamera).LookDirection}";
        }

        public event NewUiElementEventHandler NewUiElement;

        public void PressKey(KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.A:
                case Key.Left:
                    _labyrinth.Turn(TurnDirection.Left);
                    break;
                case Key.D:
                case Key.Right:
                    _labyrinth.Turn(TurnDirection.Right);
                    break;
                case Key.W:
                case Key.Up:
                    _labyrinth.Move(MoveDirection.Forward);
                    break;
                case Key.S:
                case Key.Down:
                    _labyrinth.Move(MoveDirection.Backward);
                    break;
                case Key.Q:
                    _labyrinth.Move(MoveDirection.Left);
                    break;
                case Key.E:
                    _labyrinth.Move(MoveDirection.Right);
                    break;
            }

            UpdateUi();
        }

        private void OnLoadNewElement(IUiElement element)
        {
            NewUiElement?.Invoke(this, new NewUiElementEventArgs(element));
        }
    }
}
