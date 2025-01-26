using UnityEngine;

namespace CharacterController
{
    public class SittingCharacterController : MonoBehaviour, ICharacterControllerView
    {
        [field: SerializeField] public Transform CameraTarget { get; private set; }
        [field: SerializeField] public Transform CameraFollow { get; private set; }

        public Transform Transform => transform;
        public bool IsGrounded => true;

        public void DisableMove(float threshold, bool withJump = false)
        {
            
        }

        public void EnableMove(bool withJump = false)
        {

        }

        public void Initialize(ControllerSettings settings, Transform camera)
        {

        }

        public void UpdateInputs(PlayerCharacterInputs inputs)
        {

        }
    }
}
