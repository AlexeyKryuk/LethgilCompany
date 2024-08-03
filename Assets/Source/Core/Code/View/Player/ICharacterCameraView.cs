using System.Collections.Generic;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterCameraView
    {
        Transform Transform { get; }

        void SetFollowTransform(Transform target);
        void UpdateInput(ICameraInputs inputs);
    }
}
