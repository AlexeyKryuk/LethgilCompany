using System;

namespace Core.View
{
    public interface IAnimatorController
    {
        event Action<AnimatorParameter> AnimationStarted;
        event Action<AnimatorParameter> AnimationEnded;
        event Action<AnimatorParameter> AnimationCanRepeated;

        void SetBool(AnimatorParameter parameters, bool value);
        void SetInt(AnimatorParameter parameters, int value);
        void SetFloat(AnimatorParameter parameters, float value);
        void SetTrigger(AnimatorParameter parameters);
    }
}
