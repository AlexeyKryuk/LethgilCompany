namespace Core.View
{
    public interface IAnimatorController
    {
        void SetBool(AnimatorParameter parameters, bool value);
        void SetInt(AnimatorParameter parameters, int value);
        void SetFloat(AnimatorParameter parameters, float value);
        void SetTrigger(AnimatorParameter parameters);
    }
}
