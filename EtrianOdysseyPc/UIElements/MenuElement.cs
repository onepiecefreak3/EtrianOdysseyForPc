using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EtrianOdysseyPc.UIElements
{
    public class MenuElement : IUiElement
    {
        public IDataContext DataContext { get; }
        private TextBox _text;

        public event NewUiElementEventHandler NewUiElement;

        public MenuElement()
        {
            DataContext = new ModelContext();
            SetupView();
        }

        private void SetupView()
        {
            DataContext.WindowGrid.Background = Brushes.Black;
            DataContext.WindowGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            _text = new TextBox() { Text = "From the menu" };
            DataContext.WindowGrid.Children.Add(_text);
        }

        public void PressKey(KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.A)
            {
                typeof(TextBox).GetMember("Text").In.GetProperty("Text").SetValue(_text, "Load Labyrinth");
                //_text.Text = "Load Labyrinth";
                Thread.Sleep(1000);
                OnLoadNewElement(new LabyrinthElement());
            }
        }

        private void OnLoadNewElement(IUiElement element)
        {
            NewUiElement?.Invoke(this, new NewUiElementEventArgs(element));
        }
    }
}
