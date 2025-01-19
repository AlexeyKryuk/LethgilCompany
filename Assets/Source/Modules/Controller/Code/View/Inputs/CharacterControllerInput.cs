using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementController
{
    public class CharacterControllerInput : StandaloneInputService, IInputService<PlayerCharacterInputs>
    {
        private PlayerCharacterInputs _inputs = new PlayerCharacterInputs(new Button());

        public PlayerCharacterInputs Inputs => _inputs;

        private void Update()
        {
            _inputs.ActionButton.Update(Time.deltaTime);
        }

        public void OnMove(InputValue inputValue)
        {
            _inputs.MoveAxis = inputValue.Get<Vector2>();
        }

        public void OnSprint(InputValue inputValue)
        {
            _inputs.Sprint = inputValue.Get<float>() > 0f;
        }

        public void OnJump(InputValue inputValue)
        {
            _inputs.JumpDown = inputValue.isPressed;
        }

        public void OnLMB_Down(InputValue inputValue)
        {
            _inputs.LMB_Down = inputValue.isPressed;
        }

        public void OnActionButton(InputValue inputValue)
        {
            _inputs.ActionButton.Click(inputValue.isPressed);
        }
    }
}
