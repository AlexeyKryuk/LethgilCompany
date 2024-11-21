using System;
using System.Data.Common;
using UnityEngine;

namespace Core.View
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorController<TParameter> : MonoBehaviour, IAnimatorController<TParameter> where TParameter : struct, Enum
    {
        private Animator _animator;

        public event Action<TParameter> AnimationStarted;
        public event Action<TParameter> AnimationEnded;
        public event Action<TParameter> AnimationCanRepeated;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetBool(TParameter parameter, bool value)
        {
            _animator.SetBool(parameter.ToString(), value);
        }

        public void SetFloat(TParameter parameter, float value)
        {
            _animator.SetFloat(parameter.ToString(), value);
        }

        public void SetInt(TParameter parameter, int value)
        {
            _animator.SetInteger(parameter.ToString(), value);
        }

        public void SetTrigger(TParameter parameter)
        {
            _animator.SetTrigger(parameter.ToString());
        }

        protected bool TryConvertToParameter(string name, out TParameter parameter)
        {
            return Enum.TryParse(name, out parameter);
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationStart(string name)
        {
            if (TryConvertToParameter(name, out TParameter parameter))
                AnimationStarted?.Invoke(parameter);
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationEnd(string name)
        {
            if (TryConvertToParameter(name, out TParameter parameter))
                AnimationEnded?.Invoke(parameter);
        }

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationCanRepeat(string name)
        {
            if (TryConvertToParameter(name, out TParameter parameter))
                AnimationCanRepeated?.Invoke(parameter);
        }
    }
}
