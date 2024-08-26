using Cinemachine;
using UnityEngine;
using Core.View;

namespace CharacterController
{
    public class ThirdPersonCameraController : MonoBehaviour, ICharacterCameraView
    {
        [Header("Camera Rotates around this")]
        [SerializeField] private Transform _invisibleCameraOrigin;

        [Header("Our 3rd person Vcam")]
        [SerializeField] private CinemachineVirtualCamera _vcam;

        [Header("Vertical Camera Extents")]
        [SerializeField] private float _verticalRotateMin = -80f;
        [SerializeField] private float _verticalRotateMax = 80f;

        [Header("Camera Movement Multiplier")]
        [SerializeField] private float _cameraVerticalRotationMultiplier = 2f;
        [SerializeField] private float _cameraHorizontalRotationMultiplier = 2f;

        [Header("Camera Input Values")]
        [SerializeField] private float _cameraInputHorizontal;
        [SerializeField] private float _cameraInputVertical;

        [Header("Invert Camera Controls")]
        [SerializeField] private bool _invertHorizontal = false;
        [SerializeField] private bool _invertVertical = false;

        [Header("Toggles which side the camera should start on. 1 = Right, 0 = Left")]
        [SerializeField] private float _cameraSide = 1f;

        [Header("Allow toggling left to right shoulder")]
        [SerializeField] private bool _allowCameraToggle = true;

        [Header("How fast we should transition from left to right")]
        [SerializeField] private float _cameraSideToggleSpeed = 1f;

        private float _cameraX = 0f;
        private float _cameraY = 0f;
        private float _sideToggleTime = 0f;
        private float _desiredCameraSide = 1f;
        private bool _doCameraSideToggle = false;

        private Cinemachine3rdPersonFollow _followCam;

        public Transform Transform => transform;

        private void Awake()
        {
            _followCam = _vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }

        private void Update()
        {
            if (_allowCameraToggle)
            {
                if (_doCameraSideToggle)
                {
                    _sideToggleTime = 0f;
                    _cameraSide = _followCam.CameraSide;
                    if (_cameraSide > 0.1)
                    {
                        _desiredCameraSide = 0f;
                    }
                    else
                    {
                        _desiredCameraSide = 1f;
                    }
                    _doCameraSideToggle = false;
                }

                _followCam.CameraSide = Mathf.Lerp(_cameraSide, _desiredCameraSide, _sideToggleTime);
                _sideToggleTime += _cameraSideToggleSpeed * Time.deltaTime;

            }

            if (_invisibleCameraOrigin != null)
            {
                if (_invertHorizontal)
                {
                    _cameraX -= _cameraVerticalRotationMultiplier * _cameraInputVertical;
                }
                else
                {
                    _cameraX += _cameraVerticalRotationMultiplier * _cameraInputVertical;
                }

                if (_invertVertical)
                {
                    _cameraY -= _cameraHorizontalRotationMultiplier * _cameraInputHorizontal;
                }
                else
                {
                    _cameraY += _cameraHorizontalRotationMultiplier * _cameraInputHorizontal;
                }

                _invisibleCameraOrigin.eulerAngles = new Vector3(_cameraX, _cameraY, 0.0f);
            }
        }

        public void SetFollowTransform(Transform lookAt, Transform follow)
        {
            _vcam.LookAt = lookAt;
            _vcam.Follow = follow;
        }

        public void UpdateInput(ICameraInputs inputs)
        {
            _cameraInputHorizontal = -inputs.AxisRaw.x;
            _cameraInputVertical = inputs.AxisRaw.y;
            _doCameraSideToggle = inputs.RightMouseDown;
        }

    }
}