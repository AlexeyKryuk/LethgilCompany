using CharacterController;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterCameraView
    {
        Transform Transform { get; }

        void SetFollowTransform(Transform lookAt, Transform follow);
        void UpdateInput(PlayerCameraInputs inputs);
    }
}
