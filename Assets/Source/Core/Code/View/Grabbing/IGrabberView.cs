using UnityEngine;

namespace Core.View
{
    public interface IGrabberView
    {
        void Initialize(Transform dropForward);
        void Grab();
        void Drop();
    }
}
