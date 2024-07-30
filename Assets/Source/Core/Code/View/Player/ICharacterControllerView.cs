using Core.View;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }
        Transform CameraFollowPoint { get; }

        void UpdateInputs(ICharacterInputs inputs, Quaternion cameraRotation);
    }
}
