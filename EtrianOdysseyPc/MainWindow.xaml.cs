using EtrianOdysseyPc.Interfaces;
using EtrianOdysseyPc.Managers;
using EtrianOdysseyPc.Models;
using EtrianOdysseyPc.UIElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EtrianOdysseyPc
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUiElement _uiElement;
        private ModelContext _modContext;

        public MainWindow()
        {
            _uiElement = new LabyrinthElement("testLab.eol");
            _uiElement.NewUiElement += _uiElement_NewUiElement;

            SetupView();

            DataContext = _uiElement.DataContext;

            InitializeComponent();

            //EventManager.RegisterRoutedEvent("Loaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Viewport3D));

            //DataContext = _dataContext;

            //_dataContext.LabyrinthModelGroup.Children.Add(GetModel());

            //_modelManager = new ModelManager("Ressources\\Models");
            //var m = _modelManager.RetrieveModel("testObj");
            //_dataContext.LabyrinthModelGroup.Children.Add(m);

            //var rot = new AxisAngleRotation3D(new Vector3D(0, -1, -1), 90);
            //var transform = new RotateTransform3D();
            //transform.Rotation = rot;
            //m.Transform = transform;

            //var ns = NameScope.GetNameScope(this);
            //ns.RegisterName("testObjAnim", rot);

            //var anim = new DoubleAnimation(-40, 40, new Duration(TimeSpan.FromMilliseconds(1000)));
            //// TODO: name animations and get them from files
            //Storyboard.SetTargetName(anim, "testObjAnim");
            //Storyboard.SetTargetProperty(anim, new PropertyPath("Angle"));

            //var trigger = new BeginStoryboard();
            //trigger.Storyboard = new Storyboard() { Duration = new Duration(TimeSpan.FromMilliseconds(100)), RepeatBehavior = new RepeatBehavior(1) };
            //trigger.Storyboard.Children.Add(anim);
            //trigger.Storyboard.Completed += Storyboard_Completed;

            //trigger.Storyboard.Begin();

            //var loadedTrigger = new EventTrigger(EventManager.GetRoutedEvents().First(x => x.Name == "Loaded" && x.OwnerType == typeof(Viewport3D)));
            //loadedTrigger.Actions.Add(trigger);

            //labyrinthView.Triggers.Add(loadedTrigger);
        }

        private void SetupView()
        {
            _modContext = new ModelContext();

            _modContext.WindowGrid.Background = Brushes.Wheat;
            _modContext.WindowGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            _modContext.WindowGrid.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            });

            var textBox = new TextBox() { Text = "Test" };
            Grid.SetRow(textBox, 0);
            _modContext.WindowGrid.Children.Add(textBox);

            var viewport = new Viewport3D();
            Grid.SetRow(viewport, 1);
            _modContext.WindowGrid.Children.Add(viewport);

            viewport.Camera = new PerspectiveCamera(new Point3D(5, 1, 5), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 60);

            var modelVisual1 = new ModelVisual3D();
            var mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(0, 0, 0));
            mesh.Positions.Add(new Point3D(10, 0, 0));
            mesh.Positions.Add(new Point3D(0, 0, 10));
            mesh.Positions.Add(new Point3D(10, 0, 10));
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(3);
            var model = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.AliceBlue));
            model.BackMaterial = new DiffuseMaterial(Brushes.AliceBlue);
            modelVisual1.Content = model;
            viewport.Children.Add(modelVisual1);

            var modelVisual2 = new ModelVisual3D();
            var light = new AmbientLight(Colors.White);
            modelVisual2.Content = light;
            viewport.Children.Add(modelVisual2);
        }

        private void _uiElement_NewUiElement(object sender, NewUiElementEventArgs e)
        {
            _uiElement = e.UiElement;
            _uiElement.NewUiElement += _uiElement_NewUiElement;
            DataContext = _uiElement.DataContext;
        }

        //private GeometryModel3D GetModel()
        //{
        //    var mesh = new MeshGeometry3D();
        //    mesh.Positions.Add(new Point3D(0, 0, 1));
        //    mesh.Positions.Add(new Point3D(0, 1, 1));
        //    mesh.Positions.Add(new Point3D(1, 0, 1));
        //    mesh.Positions.Add(new Point3D(1, 1, 1));
        //    mesh.TriangleIndices.Add(0);
        //    mesh.TriangleIndices.Add(2);
        //    mesh.TriangleIndices.Add(1);
        //    mesh.TriangleIndices.Add(2);
        //    mesh.TriangleIndices.Add(1);
        //    mesh.TriangleIndices.Add(3);

        //    var model = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.YellowGreen));
        //    return model;
        //}

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.A)
            //{
            //    DataContext = _uiElement.DataContext;
            //}
            //else if (e.Key == Key.S)
            //{
            //    DataContext = _modContext;
            //}
            _uiElement.PressKey(e);

            //if (e.Key == Key.Return)
            //{
            //    _dataContext.LabyrinthCam.Position -= new Vector3D(0, 0, 1);
            //}
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            _uiElement.PressKey(e);
        }

        //private void Storyboard_Completed(object sender, EventArgs e)
        //{
        //    Trace.WriteLine("SB completed.");
        //}
    }
}
