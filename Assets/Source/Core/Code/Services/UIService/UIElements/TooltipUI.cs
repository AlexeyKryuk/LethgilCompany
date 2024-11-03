using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TooltipUI : BaseUIElement
    {
        [SerializeField] private Graphic _renderer;

        private Transform _camera;

        public void Initialize(Transform camera)
        {
            _camera = camera;
        }

        public void Render(Transform target)
        {
            _renderer.enabled = target != null;

            if (target != null)
            {
                transform.position = target.position + Vector3.up;
                transform.LookAt(_camera);
            }
        }
    }
}
