using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HelixToolkit.Wpf.SharpDX;
using EtrianOdysseyPC2.Models.Labyrinth;
using EtrianOdysseyPC2.Models.Labyrinth.Format;
using Komponent.IO;
using EtrianOdysseyPC2.Models.Camera;

namespace EtrianOdysseyPC2.Managers
{
    class LabyrinthManager
    {
        private LabFile _labFile;

        public Coordinate EntryCoord => new Coordinate(_labFile.header.entryX, _labFile.header.entryY);
        public LookDirection EntryLookDirection => _labFile.header.intiDir;
        public LabCell[] Cells => _labFile.cells.cells;

        public LabyrinthManager(string labFile)
        {
            if (!File.Exists(labFile))
                throw new FileNotFoundException(labFile);

            _labFile = new BinaryReaderX(File.OpenRead(labFile)).ReadType<LabFile>();
        }

        public LabEvent[] RetrieveEvents(LabCell cell)
        {
            return _labFile.events.events.Where(x => x.cellId == cell.id).ToArray();
        }

        public string[] RetrieveModelRessources()
        {
            return _labFile.models.modelRessources.Select(x => x.value).ToArray();
        }
    }
}
