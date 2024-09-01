using System;
using UnityEngine;

namespace Core
{
    public interface IGrabberView
    {
        IAttachable Grab();
        IAttachable Drop();

        Transform Anchor { get; }
        bool IsGrabReady { get; }
        bool IsGrabActive { get; }
    }
}
