using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Customization
{
    public class NickNameView : BaseUIElement
    {
        [SerializeField] private Graphic _graphic;
        [SerializeField] private float _alphaSpeed;

        private Transform _camera;

        private Color _colorShow;
        private Color _colorHide;

        public void Initialize(Transform camera)
        {
            _camera = camera;

            _colorShow = _graphic.color;
            _colorShow.a = 1f;

            _colorHide = _graphic.color;
            _colorHide.a = 0f;
        }

        public void Show(float deltaTime)
        {
            transform.LookAt(_camera.position);

            if (_graphic.color.a < 1)
                _graphic.color = Color.Lerp(_graphic.color, _colorShow, deltaTime * _alphaSpeed);
        }

        public void Hide(float deltaTime)
        {
            if (_graphic.color.a > 0)
                _graphic.color = Color.Lerp(_graphic.color, _colorHide, deltaTime * _alphaSpeed);
        }
    }
}
