using UnityEngine;

namespace CharacterController
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }
        Transform CameraTarget { get; }
        Transform CameraFollow { get; }
        bool IsGrounded { get; }

        void UpdateInputs(PlayerCharacterInputs inputs);
        void Initialize(ControllerSettings settings, Transform camera);
        void DisableMove(float threshold, bool withJump = false);
        void EnableMove(bool withJump = false);
    }
}
