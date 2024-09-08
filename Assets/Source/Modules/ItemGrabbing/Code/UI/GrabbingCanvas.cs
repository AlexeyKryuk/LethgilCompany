using Core;
using UnityEngine;
using UnityEngine.UI;

namespace ItemGrabbing
{
    public class GrabbingCanvas : BaseUIElement
    {
        [SerializeField] private Image _circle;

        public void Render(float power)
        {
            _circle.fillAmount = Mathf.Lerp(_circle.fillAmount, 1, power * Time.deltaTime);
        }
    }
}
