using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    class LabFile
    {
        public LabHeader header;
        public LabModels models;
        public LabCells cells;
        public LabEvents events;
    }
}
