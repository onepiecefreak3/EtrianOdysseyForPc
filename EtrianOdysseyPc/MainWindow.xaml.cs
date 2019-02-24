﻿using EtrianOdysseyPc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private ModelManager _modelManager;

        public MainWindow()
        {
            EventManager.RegisterRoutedEvent("Loaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Viewport3D));
            InitializeComponent();

            _modelManager = new ModelManager("Ressources\\Models");
            var m = _modelManager.RetrieveModel("testObj");
            group.Children.Add(m);

            var rot = new AxisAngleRotation3D(new Vector3D(0, -1, -1), 90);
            var transform = new RotateTransform3D();
            transform.Rotation = rot;
            m.Transform = transform;

            var ns = NameScope.GetNameScope(this);
            ns.RegisterName("testObjAnim", rot);

            var anim = new DoubleAnimation(-40, 40, new Duration(TimeSpan.FromMilliseconds(1000)));
            // TODO: name animations and get them from files
            Storyboard.SetTargetName(anim, "testObjAnim");
            Storyboard.SetTargetProperty(anim, new PropertyPath("Angle"));

            var trigger = new BeginStoryboard();
            trigger.Storyboard = new Storyboard() { Duration = new Duration(TimeSpan.FromMilliseconds(100)), RepeatBehavior = new RepeatBehavior(1) };
            trigger.Storyboard.Children.Add(anim);
            trigger.Storyboard.Completed += Storyboard_Completed;

            trigger.Storyboard.Begin();

            //var loadedTrigger = new EventTrigger(EventManager.GetRoutedEvents().First(x => x.Name == "Loaded" && x.OwnerType == typeof(Viewport3D)));
            //loadedTrigger.Actions.Add(trigger);

            //labyrinthView.Triggers.Add(loadedTrigger);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Trace.WriteLine("SB completed.");
        }
    }
}