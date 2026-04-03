using MonoGameLibrary.General.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Graphics
{
    public class Camera : Singleton<Camera>
    {
        public Vector3 Position = new Vector3(0, 1.70f, 0);

        public float CameraViewWidth = 12.5f;
    }
}
