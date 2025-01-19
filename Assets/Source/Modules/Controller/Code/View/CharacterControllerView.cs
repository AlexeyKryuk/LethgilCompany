using UnityEngine;

namespace MovementController
{
    public class CharacterControllerView : MonoBehaviour, ICharacterControllerView
    {
        [SerializeField] private CharacterAnimatorController _characterAnimator;

        private ICharacterControllerView _personController;

        public Transform Transform => transform;
        public Transform CameraTarget => _personController.CameraTarget;
        public Transform CameraFollow => _personController.CameraFollow;
        public bool IsGrounded => _personController.IsGrounded;

        private void Awake()
        {
            foreach (var component in GetComponents<ICharacterControllerView>())
            {
                if (component is CharacterControllerView)
                    continue;

                _personController = component;
            }
        }

        public void EnableMove(bool withJump = false)
        {
            _personController.EnableMove(withJump);
        }

        public void DisableMove(float threshold, bool withJump = false)
        {
            _personController.DisableMove(threshold, withJump);
        }

        public void Initialize(ControllerSettings transformSettings, Transform camera)
        {
            _personController.Initialize(transformSettings, camera);
        }

        public void UpdateInputs(PlayerCharacterInputs inputs)
        {
            _personController.UpdateInputs(inputs);
        }
    }
}
