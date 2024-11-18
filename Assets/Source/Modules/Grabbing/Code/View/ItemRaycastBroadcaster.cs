using Core.View;
using UnityEngine;

namespace ItemGrabbing
{
    public class ItemRaycastBroadcaster : ForwardRaycastBroadcaster<AttachableItemView>
    {
        protected override bool TryGetComponent(RaycastHit hit, out AttachableItemView component)
        {
            return hit.collider.TryGetComponent(out component);
        }
    }
}
