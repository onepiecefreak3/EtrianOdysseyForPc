using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Managers
{
    public class FlagManager
    {
        private static Lazy<FlagManager> _lazy => new Lazy<FlagManager>(() => new FlagManager());
        public static FlagManager Global => _lazy.Value;

        Dictionary<string, bool> _flags;

        public FlagManager()
        {
            _flags = new Dictionary<string, bool>();
        }

        public void AddFlag(string name, bool value)
        {
            _flags.Add(name, value);
        }

        public bool IsFlagSet(string name)
        {
            return _flags[name];
        }
    }
}
