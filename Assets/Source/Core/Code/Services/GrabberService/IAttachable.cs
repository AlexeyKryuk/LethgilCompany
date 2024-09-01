using UnityEngine;

namespace Core
{
    public interface IAttachable
    {
        Transform Transform { get; }
        bool IsAttached { get; }
        void Attach(IGrabberView grabber);
        void Drop();
    }
}
