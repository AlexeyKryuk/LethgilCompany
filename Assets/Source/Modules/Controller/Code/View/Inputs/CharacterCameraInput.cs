using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterController
{
    public class CharacterCameraInput : StandaloneInputService, IInputService<PlayerCameraInputs>
    {
        private PlayerCameraInputs _inputs;

        public PlayerCameraInputs Inputs => _inputs;

        public void OnLook(InputValue inputValue)
        {
            _inputs.AxisRaw = inputValue.Get<Vector2>();
        }

        public void OnScroll(InputValue inputValue)
        {
            _inputs.Scroll = inputValue.Get<float>();
        }

        public void OnRightMouseDown(InputValue inputValue)
        {
            _inputs.RightMouseDown = inputValue.Get<bool>();
        }
    }
}
