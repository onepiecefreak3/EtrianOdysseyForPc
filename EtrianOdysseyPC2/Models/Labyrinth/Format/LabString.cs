using Komponent.IO.Attributes;

namespace EtrianOdysseyPC2.Models.Labyrinth.Format
{
    public class LabString
    {
        public byte length;
        [VariableLength("length", StringEncoding = StringEncoding.UTF8)]
        public string value;
    }
}