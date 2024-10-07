using UnityEngine;

namespace ItemGrabbing
{
    public interface IAttachableView
    {
        void Attach();
        void Unattach();
        void UpdateTransform(Vector3 position, Quaternion rotation);
        void Throw(Vector3 direction, float power);

        int NetworkId { get; }
        bool IsAvailable { get; }
    }
}
