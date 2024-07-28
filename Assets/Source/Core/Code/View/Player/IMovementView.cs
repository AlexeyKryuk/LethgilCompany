using UnityEngine;

namespace Core
{
    public interface IMovementView
    {
        Vector3 Position { get; }
        Vector3 Scale { get; }
        Quaternion Rotation { get; }

        Vector3 MoveAt(Vector3 position);
    }
}
