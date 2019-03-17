using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPC2.Managers
{
    class GlobalSettingsManager
    {
        private static Lazy<GlobalSettingsManager> _lazy = new Lazy<GlobalSettingsManager>(() => new GlobalSettingsManager());
        public static GlobalSettingsManager Global => _lazy.Value;

        public int MoveSpeed { get; private set; }
        public int TurnSpeed { get; private set; }

        public GlobalSettingsManager()
        {
            RegisterDefaultSettings();
        }

        private void RegisterDefaultSettings()
        {
            // TODO: Set global settings based on saved preferences of user
            MoveSpeed = 250;
            TurnSpeed = 250;
        }
    }
}
