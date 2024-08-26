using System.Collections.Generic;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterCameraView
    {
        Transform Transform { get; }

        void SetFollowTransform(Transform lookAt, Transform follow);
        void UpdateInput(ICameraInputs inputs);
    }
}
