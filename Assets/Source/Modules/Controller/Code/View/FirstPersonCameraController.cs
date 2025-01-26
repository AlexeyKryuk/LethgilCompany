using Cinemachine;
using Core.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class FirstPersonCameraController : MonoBehaviour, ICharacterCameraView
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public Transform Transform => transform;

        public void SetFollowTransform(Transform lookAt, Transform follow)
        {
            _virtualCamera.LookAt = lookAt;
            //_virtualCamera.Follow = follow;
        }

        public void UpdateInput(PlayerCameraInputs inputs)
        {

        }
    }
}
