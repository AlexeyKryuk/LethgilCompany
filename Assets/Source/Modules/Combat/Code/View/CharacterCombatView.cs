using CharacterController;
using Core.View;
using UnityEngine;

namespace Combat
{
    public class CharacterCombatView : MonoBehaviour, ICharacterCombatView
    {
        [SerializeField] private ParticleSystem _punchLeft;
        [SerializeField] private ParticleSystem _punchRight;

        private IAnimatorController<AnimatorParameter> _animator;
        private ICharacterControllerView _controller;

        private bool _canPunch = false;

        public Transform Transform => transform;

        private void Awake()
        {
            _animator = GetComponentInParent<IAnimatorController<AnimatorParameter>>();
        }

        private void OnEnable()
        {
            _animator.AnimationStarted += OnAnimationStart;
            _animator.AnimationCanRepeated += OnAnimationCanRepeat;
            _animator.PunchContacted += OnPunchContacted;
        }

        private void OnDisable()
        {
            _animator.AnimationStarted -= OnAnimationStart;
            _animator.AnimationCanRepeated -= OnAnimationCanRepeat;
            _animator.PunchContacted -= OnPunchContacted;
        }

        public void Initialize(ICharacterControllerView characterControllerView)
        {
            _controller = characterControllerView;
        }

        public void Start()
        {
            _canPunch = true;
        }

        public void UpdateInputs(PlayerCharacterInputs inputs)
        {
            if (inputs.LMB_Down && _controller.IsGrounded && _canPunch)
            {
                _animator.SetTrigger(Random.Range(0, 2) < 1 ? AnimatorParameter.Punch_Left : AnimatorParameter.Punch_Right);
                _canPunch = false;
            }

            inputs.LMB_Down = false;
        }

        private void OnAnimationStart(AnimatorParameter parameter)
        {
            switch (parameter)
            {
                case AnimatorParameter.Speed:
                    _controller.EnableMove(true);
                    break;

                case AnimatorParameter.Punch_Left:
                    _controller.DisableMove(0.2f, true);
                    break;

                case AnimatorParameter.Punch_Right:
                    _controller.DisableMove(0.2f, true);
                    break;

                default:
                    break;
            }
        }

        private void OnAnimationCanRepeat(AnimatorParameter parameter)
        {
            if (parameter == AnimatorParameter.Punch_Left || parameter == AnimatorParameter.Punch_Right)
                _canPunch = true;
        }

        private void OnPunchContacted(AnimatorParameter parameter)
        {
            switch (parameter)
            {
                case AnimatorParameter.Punch_Left:
                    _punchLeft.Play();
                    break;

                case AnimatorParameter.Punch_Right:
                    _punchRight.Play();
                    break;

                default:
                    break;
            }
        }
    }
}
