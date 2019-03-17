using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Managers
{
    class FlagManager
    {
        private static Lazy<FlagManager> _lazy = new Lazy<FlagManager>(() => new FlagManager());
        public static FlagManager Global => _lazy.Value;

        private Dictionary<string, bool> _flags;

        public FlagManager()
        {
            _flags = new Dictionary<string, bool>();
        }

        public void RegisterFlag(string name)
        {
            _flags.Add(name, false);
        }

        public bool GetFlag(string name)
        {
            return _flags[name];
        }

        public void SetFlag(string name, bool value)
        {
            _flags[name] = value;
        }
    }
}
