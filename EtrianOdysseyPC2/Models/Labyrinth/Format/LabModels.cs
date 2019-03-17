using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    [Alignment(0x10)]
    class LabModels
    {
        public LabModelHeader header;
        [VariableLength("header.modelCount")]
        public LabString[] modelRessources;
    }
}
