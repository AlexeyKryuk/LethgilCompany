using UnityEngine;

namespace Core
{
    public class PlayerSimpleMovementView : MonoBehaviour, IMovementView
    {
        public Vector3 Position => transform.position;
        public Vector3 Scale => transform.localScale;
        public Quaternion Rotation => transform.rotation;

        public Vector3 MoveAt(Vector3 position)
        {
            transform.position += position;

            return transform.position;
        }
    }
}
