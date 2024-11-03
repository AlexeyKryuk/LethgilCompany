using UnityEngine;

namespace Customization
{
    [RequireComponent(typeof(Camera))]
    public class Camera3DModelScaler : MonoBehaviour
    {
        [SerializeField] private float _ratio;

        private Camera _camera;
        private float _height;

        private void OnValidate()
        {
            if (_ratio == 0f)
                _ratio = 1f;
        }

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (_height != Screen.height)
            {
                _height = Screen.height;
                _camera.orthographicSize = _height / _ratio;
            }
        }
    }
}
