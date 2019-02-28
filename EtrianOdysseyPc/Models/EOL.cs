using Komponent.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Models
{
    [Endianness(ByteOrder = ByteOrder.LittleEndian)]
    public class EOL
    {
        [FixedLength(4)]
        public string labMagic;
        public byte entryX;
        public byte entryY;
        public byte entryLookDirection;
        [FixedLength(9)]
        public byte[] reserved1;

        [FixedLength(4)]
        public string cellMagic;
        public int cellCount;
        [FixedLength(8)]
        public byte[] reserved2;
    }
}
