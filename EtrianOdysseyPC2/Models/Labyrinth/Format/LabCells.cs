using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    [Alignment(0x10)]
    class LabCells
    {
        public LabCellHeader header;
        [VariableLength("header.cellCount")]
        public LabCell[] cells;
    }
}
