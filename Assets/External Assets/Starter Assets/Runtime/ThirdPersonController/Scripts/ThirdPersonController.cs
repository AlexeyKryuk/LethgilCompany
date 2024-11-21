using CharacterController;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StarterAssets
{
    public class ThirdPersonController : MonoBehaviour, ICharacterControllerView
    {
        [Header("Player")]
        public Transform TransformParent;

        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 1.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        public GameObject CinemachineCameraFollow;
        public GameObject CinemachineCameraAim;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        [SerializeField] private UnityEngine.CharacterController _controller;
        [SerializeField] private CharacterAnimatorController _animator;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        private GameObject _characterCamera;

        private bool _hasAnimator = true;
        private bool _canJump = true;

        public float SpeedDelimeter { get; set; } = 1f;

        public Transform Transform => TransformParent;
        public Transform CameraTarget => CinemachineCameraTarget.transform;
        public Transform CameraFollow => CinemachineCameraFollow.transform;
        public bool IsGrounded => Grounded;

        private void OnEnable()
        {
            _animator.Landed += OnLand;
            _animator.Footstep += OnFootstep;
        }

        private void OnDisable()
        {
            _animator.Landed -= OnLand;
            _animator.Footstep -= OnFootstep;
        }

        private void Start()
        {
            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        public void UpdateCamera()
        {
            CinemachineCameraAim.transform.position = _characterCamera.transform.position + _characterCamera.transform.forward * 6f;
        }

        public void DisableMove(float threshold, bool withJump = false)
        {
            SpeedDelimeter = threshold;

            if (withJump)
                _canJump = false;
        }

        public void EnableMove(bool withJump = false)
        {
            SpeedDelimeter = 1f;

            if (withJump)
            {
                _canJump = true;
                _jumpTimeoutDelta = JumpTimeout;
            }
        }

        public void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(TransformParent.position.x, TransformParent.position.y - GroundedOffset,
                TransformParent.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(CharacterControllerAnimatorParameter.Grounded, Grounded);
            }
        }

        public void Move(Vector2 moveAxis, bool sprint)
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (moveAxis == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(moveAxis.x, 0.0f, moveAxis.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (moveAxis != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _characterCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(TransformParent.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                TransformParent.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * SpeedDelimeter * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(CharacterControllerAnimatorParameter.Speed, _animationBlend);
                _animator.SetFloat(CharacterControllerAnimatorParameter.MotionSpeed, inputMagnitude);
            }
        }

        public void JumpAndGravity(bool jumpButtonDown)
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(CharacterControllerAnimatorParameter.Jump, false);
                    _animator.SetBool(CharacterControllerAnimatorParameter.FreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (jumpButtonDown && _jumpTimeoutDelta <= 0.0f && _canJump)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(CharacterControllerAnimatorParameter.Jump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f && _canJump)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(CharacterControllerAnimatorParameter.FreeFall, true);
                    }
                }
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(TransformParent.position.x, TransformParent.position.y - GroundedOffset, TransformParent.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], TransformParent.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, TransformParent.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public void UpdateInputs(PlayerCharacterInputs inputs)
        {
            JumpAndGravity(inputs.JumpDown);
            GroundedCheck();
            Move(inputs.MoveAxis, inputs.Sprint);
            UpdateCamera();
        }

        public void Initialize(ControllerSettings settings, Transform camera)
        {
            MoveSpeed = settings.Speed.Walk;
            SprintSpeed = settings.Speed.Sprint;
            JumpHeight = settings.Jumping.Height;
            Gravity = settings.Jumping.Gravity;
            JumpTimeout = settings.Jumping.Timeout;
            FallTimeout = settings.Jumping.FallTimeout;

            _characterCamera = camera.gameObject;
        }
    }
}