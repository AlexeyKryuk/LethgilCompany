using Core.View;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class StandaloneInputService : MonoBehaviour, IInputService
    {
        public ICharacterInputs CharacterInputs { get; private set; }
        public ICameraInputs CameraInputs { get; private set; }

        public event Action Jump;

        private void Awake()
        {
            CharacterInputs = new PlayerCharacterInputs();
            CameraInputs = new PlayerCameraInputs();
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

        private void OnApplicationFocus(bool focus)
        {
            Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !focus;
        }
    }
}
