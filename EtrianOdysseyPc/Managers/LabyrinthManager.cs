using EtrianOdysseyPc.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtrianOdysseyPc.Managers
{
    public class LabyrinthsManager
    {
        private string _labPath;

        public LabyrinthsManager(string labPath)
        {
            if (!Directory.Exists(labPath))
                throw new DirectoryNotFoundException(labPath);

            _labPath = labPath;
        }

        public LabyrinthProvider LoadLabyrinth(string name)
        {
            return new LabyrinthProvider(Path.Combine(_labPath,name));
        }
    }
}
