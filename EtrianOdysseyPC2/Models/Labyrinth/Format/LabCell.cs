﻿using Komponent.IO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    [Alignment(0x4)]
    class LabCell
    {
        public int x;
        public int y;
        public short id;
        public byte level;
    }
}
