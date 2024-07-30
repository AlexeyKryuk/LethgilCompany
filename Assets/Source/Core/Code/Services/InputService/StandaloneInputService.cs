using Core.View;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
            CharacterInputs.MoveAxisForward = inputValue.Get<Vector2>().y;
            CharacterInputs.MoveAxisRight = inputValue.Get<Vector2>().x;
        }

        public void OnJump(InputValue inputValue)
        {
            CharacterInputs.JumpDown = inputValue.Get<bool>();
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
    }
}
