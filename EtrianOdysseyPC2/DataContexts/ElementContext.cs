using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EtrianOdysseyPC2.DataContexts
{
    class ElementContext
    {
        public Grid Grid { get; set; }

        public ElementContext()
        {
            Grid = new Grid();
        }
    }
}
