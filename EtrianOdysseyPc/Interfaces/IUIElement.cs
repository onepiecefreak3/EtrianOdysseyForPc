using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EtrianOdysseyPc.Interfaces
{
    public interface IUiElement
    {
        IDataContext DataContext { get; }

        event NewUiElementEventHandler NewUiElement;

        void PressKey(KeyEventArgs keyEventArgs);
    }

    public delegate void NewUiElementEventHandler(object sender, NewUiElementEventArgs e);

    public class NewUiElementEventArgs
    {
        public IUiElement UiElement { get; set; }

        public NewUiElementEventArgs(IUiElement element)
        {
            UiElement = element;
        }
    }
}
