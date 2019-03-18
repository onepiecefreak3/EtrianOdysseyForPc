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
        ModelContext _mainContext = new ModelContext();

        public MainWindow()
        {
            DataContext = _mainContext;
            SetupUiElement(_currentUiElement);

            InitializeComponent();
        }

        private void SetupUiElement(IUiElement element)
        {
            _currentUiElement = element;
            _currentUiElement.SwitchUiElement += Element_SwitchUiElement;

            _mainContext.Grid = element.ElementContext.Grid;
        }

        private void Element_SwitchUiElement(object sender, EventHandlers.SwitchUiElementEventArgs e)
        {
            Logging.Logger.Global.Information(_mainContext, $"Switch to UiElement {e.UiElement.Name}");
            SetupUiElement(e.UiElement);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Logging.Logger.Global.Information(_mainContext, $"Key pressed: {e.Key}");
            _currentUiElement.KeyPress(e);
        }
    }
}
