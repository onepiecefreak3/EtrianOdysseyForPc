using EtrianOdysseyPc.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc.Models
{
    public class ModelContext : IDataContext
    {
        public Grid WindowGrid { get; set; }

        public ModelContext()
        {
            WindowGrid = new Grid();
        }
    }
}
