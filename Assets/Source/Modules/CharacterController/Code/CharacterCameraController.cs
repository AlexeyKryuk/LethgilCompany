using UnityEngine;
using Cinemachine;
using Core.View;

namespace CharacterController
{
    public class CharacterCameraController : MonoBehaviour, ICharacterCameraView
    {
        [SerializeField] private CinemachineFreeLook _freeLookCamera;

        public Transform Transform => transform;

        public void SetFollowTransform(Transform lookAt, Transform follow)
        {
            _freeLookCamera.LookAt = lookAt;
            _freeLookCamera.Follow = follow;
        }

        public void UpdateInput(ICameraInputs inputs)
        {

        }
    }
}
