using System;
using UnityEngine;
using UnityEngine.UI;

namespace Customization
{
    public class NicknameConfirmButton : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _loginButton;

        public event Action<string> NicknameConfirmed;

        private void OnEnable()
        {
            _loginButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _loginButton.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            NicknameConfirmed?.Invoke(_inputField.text);
        }
    }
}
