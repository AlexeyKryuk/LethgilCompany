using System;

namespace Core.View
{
    public interface IAnimatorController<T> where T : Enum
    {
        event Action<T> AnimationStarted;
        event Action<T> AnimationEnded;
        event Action<T> AnimationCanRepeated;
        event Action<T> PunchContacted;

        void SetBool(T parameters, bool value);
        void SetInt(T parameters, int value);
        void SetFloat(T parameters, float value);
        void SetTrigger(T parameters);
    }
}
