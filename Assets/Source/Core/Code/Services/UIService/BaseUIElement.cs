using UnityEngine;

namespace Core
{
    public abstract class BaseUIElement : MonoBehaviour, IUIElement
    {
        [field: SerializeField]
        public UIElementID UIElementType { get; private set; }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
