using UnityEngine;

namespace Core.View
{
    public interface ICameraInputs
    {
        Vector2 AxisRaw { get; set; }
        float Scroll { get; set; }
        bool RightMouseDown { get; set; }
    }
}
