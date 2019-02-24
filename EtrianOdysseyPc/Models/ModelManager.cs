using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc.Models
{
    public class ModelManager
    {
        private Dictionary<string, GeometryModel3D> _models;
        private string _modelPath;

        public ModelManager(string modelPath)
        {
            if (!Directory.Exists(modelPath))
                throw new DirectoryNotFoundException(modelPath);

            _modelPath = modelPath;
            _models = new Dictionary<string, GeometryModel3D>();

            LoadModels();
        }

        private void LoadModels()
        {
            foreach (var file in Directory.EnumerateFiles(_modelPath))
            {
                var eom = new EOM(file);
                _models.Add(eom.RetrieveName(), eom.RetrieveModel());
            }
        }

        public GeometryModel3D RetrieveModel(string name)
        {
            if (!_models.ContainsKey(name))
                throw new KeyNotFoundException($"Model \"{name}\" wasn't found");

            return _models[name];
        }
    }
}
