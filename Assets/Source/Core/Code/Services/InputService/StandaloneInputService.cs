using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class StandaloneInputService : MonoBehaviour, IInputService
    {
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        public event Action Jump;

        public void OnMove(InputValue inputValue)
        {
            Move = inputValue.Get<Vector2>();
        }

        public void OnJump(InputValue inputValue)
        {
            Jump?.Invoke();
        }

        public void OnLook(InputValue inputValue)
        {
            Look = inputValue.Get<Vector2>();
        }
    }
}
