using Core.View;
using System;

namespace Combat
{
    public class CombatAnimatorController : AnimatorController<CombatAnimatorParameter>
    {
        public event Action<CombatAnimatorParameter> PunchContacted;

        /// <summary>
        /// AnimationEvent
        /// </summary>
        /// <param name="name"></param>
        private void OnPunchContacted(string name)
        {
            if (TryConvertToParameter(name, out CombatAnimatorParameter parameter))
                PunchContacted?.Invoke(parameter);
        }
    }
}
