using Core.Model;
using System.Collections;
using UnityEngine;

namespace Core.View
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }
        Transform CameraTarget { get; }
        Transform CameraFollow { get; }
        bool IsGrounded { get; }

        void UpdateInputs(ICharacterInputs inputs);
        void SetCameraTransform(Transform camera);
        void Initialize(TransformSettings transform);
        IEnumerator SetSpeedDelimeter(float value, float valueChangeSpeed);
    }
}
