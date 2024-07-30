using Core.View;
using UnityEngine;

namespace Core
{
    public class PlayerCameraInputs : ICameraInputs
    {
        public Vector2 AxisRaw { get; set; }
        public float Scroll { get; set; }
        public bool RightMouseDown { get; set; }
    }
}
