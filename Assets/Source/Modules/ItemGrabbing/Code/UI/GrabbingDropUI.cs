using Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ItemGrabbing
{
    public class GrabbingDropUI : BaseUIElement
    {
        [SerializeField] private Image _circle;

        public void Render(float targetValue, float holdTime)
        {
            if (targetValue == 0)
                throw new ArgumentNullException();

            _circle.fillAmount = holdTime / targetValue;
        }
    }
}
