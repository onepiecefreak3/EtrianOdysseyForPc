using EtrianOdysseyPC2.Managers;
using EtrianOdysseyPC2.Models.Camera;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace EtrianOdysseyPC2.Cameras
{
    class PlayerCamera : PerspectiveCamera
    {
        private float _tileSize;
        private double _playerHeight;

        public LookDirection CurrentLookDirection { get; private set; }

        public bool IsAnimated { get; private set; }

        public PlayerCamera(int entryX, int entryY, LookDirection lookDir, float tileSize) : base()
        {
            _tileSize = tileSize;
            _playerHeight = 3;

            Position = new System.Windows.Media.Media3D.Point3D(entryX + _tileSize / 2, _playerHeight, entryY + _tileSize / 2);
            LookDirection = GetLookDirectionVector(lookDir);
            CurrentLookDirection = lookDir;
        }

        public void Move(LookDirection dir)
        {
            Point3DAnimation anim = new Point3DAnimation(Position + GetMoveVector(dir), new System.Windows.Duration(TimeSpan.FromMilliseconds(GlobalSettingsManager.Global.TurnSpeed)));

            anim.Completed += Anim_Completed;
            IsAnimated = true;
            BeginAnimation(PerspectiveCamera.PositionProperty, anim);
        }

        public void Turn(LookDirection dir)
        {
            CurrentLookDirection = dir;

            var anim = new Vector3DAnimation(GetLookDirectionVector(dir), new System.Windows.Duration(TimeSpan.FromMilliseconds(GlobalSettingsManager.Global.TurnSpeed)));

            anim.Completed += Anim_Completed;
            IsAnimated = true;
            BeginAnimation(PerspectiveCamera.PositionProperty, anim);
        }

        private void Anim_Completed(object sender, EventArgs e)
        {
            IsAnimated = false;
        }

        private System.Windows.Media.Media3D.Vector3D GetLookDirectionVector(LookDirection dir)
        {
            switch (dir)
            {
                case Models.Camera.LookDirection.North:
                    return new System.Windows.Media.Media3D.Vector3D(0, 0, -1);
                case Models.Camera.LookDirection.East:
                    return new System.Windows.Media.Media3D.Vector3D(1, 0, 0);
                case Models.Camera.LookDirection.South:
                    return new System.Windows.Media.Media3D.Vector3D(0, 0, 1);
                case Models.Camera.LookDirection.West:
                    return new System.Windows.Media.Media3D.Vector3D(-1, 0, 0);
                default:
                    throw new InvalidOperationException($"Unknown direction {dir}");
            }
        }

        private System.Windows.Media.Media3D.Vector3D GetMoveVector(LookDirection dir)
        {
            switch (dir)
            {
                case Models.Camera.LookDirection.North:
                    return new System.Windows.Media.Media3D.Vector3D(0, 0, _tileSize);
                case Models.Camera.LookDirection.East:
                    return new System.Windows.Media.Media3D.Vector3D(_tileSize, 0, 0);
                case Models.Camera.LookDirection.South:
                    return new System.Windows.Media.Media3D.Vector3D(0, 0, -_tileSize);
                case Models.Camera.LookDirection.West:
                    return new System.Windows.Media.Media3D.Vector3D(-_tileSize, 0, 0);
                default:
                    throw new InvalidOperationException($"Unknown direction {dir}");
            }
        }

        public void Teleport(int x, int y)
        {
            Position = new System.Windows.Media.Media3D.Point3D(x * _tileSize - _tileSize / 2, _playerHeight, y * _tileSize - _tileSize / 2);
        }
    }
}
