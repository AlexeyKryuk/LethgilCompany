using UnityEngine;

namespace Core.View
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }
        Transform CameraTarget { get; }
        Transform CameraFollow { get; }

        void SpecifyCameraTransform(Transform transform);
        void UpdateInputs(ICharacterInputs inputs);
    }
}
