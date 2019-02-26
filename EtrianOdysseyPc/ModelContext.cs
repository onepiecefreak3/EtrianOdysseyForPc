using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace EtrianOdysseyPc
{
    public class ModelContext
    {
        public PerspectiveCamera LabyrinthCam { get; set; }
        public Model3DGroup LabyrinthModelGroup { get; set; }

        public ModelContext()
        {
            LabyrinthCam = new PerspectiveCamera() {
                Position = new Point3D(0.5, 0.5, 4),
                LookDirection = new Vector3D(0, 0, -3.5)
            };

            LabyrinthModelGroup = new Model3DGroup();
            //LabyrinthModelGroup.Children.Add(new AmbientLight(Colors.DarkGray));
            //LabyrinthModelGroup.Children.Add(new DirectionalLight(Colors.White, new Vector3D(-5, -5, -7)));
        }
    }
}
