using UnityEngine;

namespace Core.View
{
    public class CharacterCombatView : MonoBehaviour, ICharacterCombatView
    {
        [SerializeField] private float _maxValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _valueChangeSpeed;

        private IAnimatorController _animator;
        private ICharacterControllerView _controller;

        private bool _canPunch = false;

        public Transform Transform => transform;

        private void Awake()
        {
            _animator = GetComponentInParent<IAnimatorController>();
        }

        private void OnEnable()
        {
            _animator.AnimationStarted += OnAnimationStart;
            _animator.AnimationCanRepeated += OnAnimationCanRepeat;
        }

        private void OnDisable()
        {
            _animator.AnimationStarted -= OnAnimationStart;
            _animator.AnimationCanRepeated -= OnAnimationCanRepeat;
        }

        public void Initialize(ICharacterControllerView characterControllerView)
        {
            _controller = characterControllerView;
        }

        public void Start()
        {
            _canPunch = true;
        }

        public void UpdateInputs(ICharacterInputs inputs)
        {
            if (inputs.LMB_Down && _controller.IsGrounded && _canPunch)
            {
                _animator.SetFloat(AnimatorParameter.Random, Random.Range(0, 2));
                _animator.SetTrigger(AnimatorParameter.Punch);

                _canPunch = false;
            }

            inputs.LMB_Down = false;
        }

        private void OnAnimationStart(AnimatorParameter parameter)
        {
            switch (parameter)
            {
                case AnimatorParameter.Speed:
                    _controller.SpeedDelimeter = 1f;
                    break;

                case AnimatorParameter.Punch:
                    _controller.SpeedDelimeter = 0.15f;
                    break;

                default:
                    break;
            }
        }

        private void OnAnimationCanRepeat(AnimatorParameter parameter)
        {
            if (parameter != AnimatorParameter.Punch)
                return;

            _canPunch = true;
        }
    }
}
