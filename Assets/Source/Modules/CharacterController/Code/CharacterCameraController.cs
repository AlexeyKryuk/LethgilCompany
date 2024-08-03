using UnityEngine;
using Cinemachine;
using Core.View;

namespace CharacterController
{
    public class CharacterCameraController : MonoBehaviour, ICharacterCameraView
    {
        [SerializeField] private CinemachineFreeLook _freeLookCamera;

        public Transform Transform => transform;

        public void SetFollowTransform(Transform target)
        {
            _freeLookCamera.LookAt = target;
            _freeLookCamera.Follow = target;
        }

        public void UpdateInput(ICameraInputs inputs)
        {

        }
    }
}
