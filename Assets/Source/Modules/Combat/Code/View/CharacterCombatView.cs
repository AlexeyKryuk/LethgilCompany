using CharacterController;
using UnityEngine;

namespace Combat
{
    public class CharacterCombatView : MonoBehaviour, ICharacterCombatView
    {
        [Header("Animators")]
        [SerializeField] private CombatAnimatorController _combatAnimator;
        [SerializeField] private CharacterAnimatorController _movementAnimator;

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem _punchLeft;
        [SerializeField] private ParticleSystem _punchRight;

        private ICharacterControllerView _controller;

        private bool _canPunch = false;

        public Transform Transform => transform;

        private void OnEnable()
        {
            _combatAnimator.AnimationStarted += OnCombatAnimationStart;
            _movementAnimator.AnimationStarted += OnMoveAnimationStart;
            _combatAnimator.AnimationCanRepeated += OnAnimationCanRepeat;
            _combatAnimator.PunchContacted += OnPunchContacted;
        }

        private void OnDisable()
        {
            _combatAnimator.AnimationStarted -= OnCombatAnimationStart;
            _movementAnimator.AnimationStarted -= OnMoveAnimationStart;
            _combatAnimator.AnimationCanRepeated -= OnAnimationCanRepeat;
            _combatAnimator.PunchContacted -= OnPunchContacted;
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
                _combatAnimator.SetTrigger(Random.Range(0, 2) < 1 ? CombatAnimatorParameter.Punch_Left : CombatAnimatorParameter.Punch_Right);
                _canPunch = false;
            }

            inputs.LMB_Down = false;
        }

        private void OnMoveAnimationStart(CharacterControllerAnimatorParameter parameter)
        {
            if (parameter == CharacterControllerAnimatorParameter.Speed)
                _controller.EnableMove(true);
        }

        private void OnCombatAnimationStart(CombatAnimatorParameter parameter)
        {
            switch (parameter)
            {
                case CombatAnimatorParameter.Punch_Left:
                    _controller.DisableMove(0.2f, true);
                    break;

                case CombatAnimatorParameter.Punch_Right:
                    _controller.DisableMove(0.2f, true);
                    break;

                default:
                    break;
            }
        }

        private void OnAnimationCanRepeat(CombatAnimatorParameter parameter)
        {
            if (parameter == CombatAnimatorParameter.Punch_Left || parameter == CombatAnimatorParameter.Punch_Right)
                _canPunch = true;
        }

        private void OnPunchContacted(CombatAnimatorParameter parameter)
        {
            switch (parameter)
            {
                case CombatAnimatorParameter.Punch_Left:
                    _punchLeft.Play();
                    break;

                case CombatAnimatorParameter.Punch_Right:
                    _punchRight.Play();
                    break;

                default:
                    break;
            }
        }
    }
}
