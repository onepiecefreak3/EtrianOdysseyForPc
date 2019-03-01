using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EtrianOdysseyPc.UIElements
{
    internal class LabyrinthElement : IUiElement
    {
        public IDataContext DataContext { get; }
        private TextBox _text;

        public LabyrinthElement()
        {
            DataContext = new ModelContext();
            SetupView();
        }

        private void SetupView()
        {
            _text = new TextBox() { Text = "From the labyrinth" };
            DataContext.WindowGrid.Children.Add(_text);
        }

        public event NewUiElementEventHandler NewUiElement;

        public void PressKey(KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.A)
            {
                _text.Text = "Load Menu";
                Thread.Sleep(1000);
                OnLoadNewElement(new MenuElement());
            }
        }

        private void OnLoadNewElement(IUiElement element)
        {
            NewUiElement?.Invoke(this, new NewUiElementEventArgs(element));
        }
    }
}
