using Core.Model;
using StarterAssets;
using UnityEngine;

namespace CharacterController
{
    public class CharacterControllerView : MonoBehaviour, ICharacterControllerView
    {
        [SerializeField] private ThirdPersonController _personController;

        public Transform Transform => transform;
        public Transform CameraTarget => _personController.CinemachineCameraTarget.transform;
        public Transform CameraFollow => _personController.CinemachineCameraFollow.transform;
        public bool IsGrounded => _personController.Grounded;

        public void EnableMove(bool withJump = false)
        {
            _personController.EnableMove(withJump);
        }

        public void DisableMove(float threshold, bool withJump = false)
        {
            _personController.DisableMove(threshold, withJump);
        }

        public void Initialize(ControllerSettings transformSettings)
        {
            _personController.MoveSpeed = transformSettings.Speed.Walk;
            _personController.SprintSpeed = transformSettings.Speed.Sprint;
            _personController.JumpHeight = transformSettings.Jumping.Height;
            _personController.Gravity = transformSettings.Jumping.Gravity;
            _personController.JumpTimeout = transformSettings.Jumping.Timeout;
            _personController.FallTimeout = transformSettings.Jumping.FallTimeout;
        }

        public void SetCameraTransform(Transform camera)
        {
            _personController.SetCameraTransform(camera.gameObject);
        }

        public void UpdateInputs(PlayerCharacterInputs inputs)
        {
            _personController.JumpAndGravity(inputs.JumpDown);
            _personController.GroundedCheck();
            _personController.Move(inputs.MoveAxis, inputs.Sprint);
            _personController.UpdateCamera();
        }
    }
}
