using EtrianOdysseyPC2.DataContexts;
using EtrianOdysseyPC2.Interfaces;
using EtrianOdysseyPC2.UiElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EtrianOdysseyPC2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUiElement _currentUiElement = new GameChoiceElement();
        ModelContext _context;

        public MainWindow()
        {
            SetupUiElement(_currentUiElement);

            InitializeComponent();
        }

        private void SetupUiElement(IUiElement element)
        {
            element.SwitchUiElement += Element_SwitchUiElement;

            _context = element.DataContext;
        }

        private void Element_SwitchUiElement(object sender, EventHandlers.SwitchUiElementEventArgs e)
        {
            SetupUiElement(e.UiElement);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            _currentUiElement.KeyPress(e);
        }
    }
}
