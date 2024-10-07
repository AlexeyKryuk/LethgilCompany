using Core;
using UnityEngine;
using UnityEngine.UI;

namespace ItemGrabbing
{
    public class GrabbingTooltipUI : BaseUIElement
    {
        [SerializeField] private Graphic _renderer;

        private Transform _camera;

        public void Initialize(Transform camera)
        {
            _camera = camera;
        }

        public void Render(AttachableItemView target)
        {
            _renderer.enabled = target != null;

            if (target != null)
            {
                transform.position = target.transform.position + Vector3.up;
                transform.LookAt(_camera);
            }
        }
    }
}
