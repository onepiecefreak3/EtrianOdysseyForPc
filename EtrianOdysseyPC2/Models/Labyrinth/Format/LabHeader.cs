using EtrianOdysseyPC2.Models.Camera;
using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    [Alignment(0x10)]
    class LabHeader
    {
        [FixedLength(4)]
        public string magic;
        public int entryX;
        public int entryY;
        public LookDirection intiDir;
    }
}
