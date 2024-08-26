using Core.View;
using UnityEngine;

namespace CharacterController
{
    public class CharacterCameraControllerSimple : MonoBehaviour, ICharacterCameraView
    {
        public Transform Transform => transform;

        public void SetFollowTransform(Transform lookAt, Transform follow) { }

        public void UpdateInput(ICameraInputs inputs) { }
    }
}
