using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Customization
{
    public class NickNameView : BaseUIElement
    {
        [SerializeField] private Text _text;
        [SerializeField] private Graphic _graphic;
        [SerializeField] private float _alphaSpeed;

        private Transform _camera;

        private Color _colorShow;
        private Color _colorHide;
        private Color _targetColor;

        public void Initialize(Transform camera, string nickName)
        {
            _camera = camera;
            _text.text = nickName;

            _colorShow = _graphic.color;
            _colorShow.a = 1f;

            _colorHide = _graphic.color;
            _colorHide.a = 0f;

            _targetColor = _colorHide;
        }

        public void Show()
        {
            _targetColor = _colorShow;
        }

        public void Hide()
        {
            _targetColor = _colorHide;
        }

        private void Update()
        {
            if (_graphic.color != _targetColor)
                _graphic.color = Color.Lerp(_graphic.color, _targetColor, Time.deltaTime * _alphaSpeed);

            transform.LookAt(_camera.position);
        }
    }
}
