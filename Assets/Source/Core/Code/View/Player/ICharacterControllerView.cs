using UnityEngine;

namespace Core.View
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }
        Transform CameraTarget { get; }
        Transform CameraFollow { get; }

        void UpdateInputs(ICharacterInputs inputs);
        void SetCameraTransform(Transform camera);
    }
}
