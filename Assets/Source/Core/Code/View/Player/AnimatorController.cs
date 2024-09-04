using UnityEngine;

namespace Core.View
{
    public class AnimatorController : MonoBehaviour, IAnimatorController
    {
        [SerializeField] private Animator _animator;

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
    }
}
