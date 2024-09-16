using Core.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class StandaloneInputService : MonoBehaviour, IInputService
    {
        public ICharacterInputs CharacterInputs { get; private set; }
        public ICameraInputs CameraInputs { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            CharacterInputs = new PlayerCharacterInputs();
            CameraInputs = new PlayerCameraInputs();
        }

        private void Update()
        {
            CharacterInputs.ActionButton.Update(Time.deltaTime);
        }

        public void OnMove(InputValue inputValue)
        {
            CharacterInputs.MoveAxis = inputValue.Get<Vector2>();
        }

        public void OnSprint(InputValue inputValue)
        {
            CharacterInputs.Sprint = inputValue.Get<float>() > 0f;
        }

        public void OnJump(InputValue inputValue)
        {
            CharacterInputs.JumpDown = inputValue.isPressed;
        }

        public void OnLook(InputValue inputValue)
        {
            CameraInputs.AxisRaw = inputValue.Get<Vector2>();
        }

        public void OnScroll(InputValue inputValue)
        {
            CameraInputs.Scroll = inputValue.Get<float>();
        }

        public void OnRightMouseDown(InputValue inputValue)
        {
            CameraInputs.RightMouseDown = inputValue.Get<bool>();
        }

        public void OnActionButton(InputValue inputValue)
        {
            CharacterInputs.ActionButton.Click(inputValue.isPressed);
        }
    }
}
