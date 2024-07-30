using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterCameraView
    {
        Transform Transform { get; }
        List<Collider> IgnoredColliders { get; }

        void SetFollowTransform(Transform target);
        void UpdateInput(ICameraInputs inputs);
    }
}
