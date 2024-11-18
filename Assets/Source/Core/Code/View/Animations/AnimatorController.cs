using System;
using UnityEngine;

namespace Core.View
{
    public class AnimatorController : MonoBehaviour, IAnimatorController<AnimatorParameter>
    {
        [SerializeField] private Animator _animator;

        public event Action<AnimatorParameter> AnimationStarted;
        public event Action<AnimatorParameter> AnimationEnded;
        public event Action<AnimatorParameter> AnimationCanRepeated;
        public event Action<AnimatorParameter> PunchContacted;

        public void SetBool(AnimatorParameter parameter, bool value)
        {
            _animator.SetBool(parameter.ToString(), value);
        }

        public void SetFloat(AnimatorParameter parameter, float value)
        {
            _animator.SetFloat(parameter.ToString(), value);
        }

        public void SetInt(AnimatorParameter parameter, int value)
        {
            _animator.SetInteger(parameter.ToString(), value);
        }

        public void SetTrigger(AnimatorParameter parameter)
        {
            _animator.SetTrigger(parameter.ToString());
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationStart(string name)
        {
            AnimationStarted?.Invoke(ConvertToParameter(name));
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationEnd(string name)
        {
            AnimationEnded?.Invoke(ConvertToParameter(name));
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationCanRepeat(string name)
        {
            AnimationCanRepeated?.Invoke(ConvertToParameter(name));
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnPunchContacted(string name)
        {
            PunchContacted?.Invoke(ConvertToParameter(name));
        }

        private AnimatorParameter ConvertToParameter(string name)
        {
            if (Enum.TryParse(name, out AnimatorParameter parameter))
                return parameter;

            throw new ArgumentException($"Invalid animator parameter [{name}]");
        }
    }
}
