using Core;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class UIElementPhotonBehaviour : MonoBehaviourPunCallbacks, IUIElement
    {
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
