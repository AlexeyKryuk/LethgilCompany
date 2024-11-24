using UnityEngine;

namespace ItemGrabbing
{
    public interface IGrabberView
    {
        void Initialize(GrabbingData model, Transform directionOfView);
        void Grab(IAttachableView item);
        void Drop(float holdTime);

        Transform Anchor { get; }
        Transform DirectionOfView { get; }
    }
}
