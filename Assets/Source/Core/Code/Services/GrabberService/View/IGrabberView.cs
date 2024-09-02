using UnityEngine;

namespace Core
{
    public interface IGrabberView
    {
        IAttachableView Grab();
        IAttachableView Drop();

        Transform Anchor { get; }
        bool IsGrabReady { get; }
        bool IsGrabActive { get; }
    }
}
