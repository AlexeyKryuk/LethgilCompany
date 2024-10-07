using UnityEngine;

namespace ItemGrabbing
{
    public interface IGrabberView
    {
        void Initialize(GrabbingConfig config, Transform directionOfView);
        void Grab(IAttachableView item);
        void Drop(float holdTime);

        Transform Anchor { get; }
        Transform DirectionOfView { get; }
    }
}
