using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EtrianOdysseyPC2.DataContexts
{
    class ModelContext
    {
        public Grid Grid { get; set; }

        public ModelContext()
        {
            Grid = new Grid();
        }
    }
}
