using Core.View;
using UnityEngine;

namespace Network
{
    public class CustomizationViewRaycastBroadcaster : ForwardRaycastBroadcaster<CustomizationPhotonView>
    {
        protected override bool TryGetComponent(RaycastHit hit, out CustomizationPhotonView component)
        {
            component = hit.collider.GetComponentInChildren<CustomizationPhotonView>();

            return component != null;
        }
    }
}
