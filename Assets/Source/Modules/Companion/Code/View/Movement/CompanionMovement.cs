using UnityEngine;

namespace Companion
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CompanionAnimatorController))]
    public class CompanionMovement : MonoBehaviour
    {
        [Tooltip("Move speed of the character in m/s")]
        [SerializeField] private float _moveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        [SerializeField] private float _sprintSpeed = 5.335f;

        [Tooltip("Acceleration and deceleration")]
        [SerializeField] private float _speedChangeRate = 10.0f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        [SerializeField] private float _rotationSmoothTime = 0.12f;

        private CharacterController _controller;
        private CompanionAnimatorController _animator;

        private float _speed;
        private float _animationBlend;
        private float _targetRotation;
        private float _rotationVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<CompanionAnimatorController>();
        }

        public void UpdateInput(Vector2 moveAxis, bool sprint)
        {
            float targetSpeed = sprint ? _sprintSpeed : _moveSpeed;
            float speedOffset = 0.1f;

            if (moveAxis == Vector2.zero)
                targetSpeed = 0.0f;

            Rotate(moveAxis);
            Move(targetSpeed, speedOffset);
            Animate(targetSpeed);
        }

        private void Move(float targetSpeed, float speedOffset)
        {
            float currentHorizontalSpeed =
                new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed,
                    Time.deltaTime * _speedChangeRate);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _controller.Move(targetDirection.normalized * _speed * Time.deltaTime);
        }

        private void Rotate(Vector2 moveAxis)
        {
            Vector3 inputDirection = new Vector3(moveAxis.x, 0.0f, moveAxis.y).normalized;

            if (moveAxis != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation,
                    ref _rotationVelocity, _rotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        private void Animate(float targetSpeed)
        {
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _speedChangeRate);

            if (_animationBlend < 0.01f)
                _animationBlend = 0f;

            _animator.SetFloat(CompanionAnimatorParameter.Speed, _animationBlend);
        }
    }
}
