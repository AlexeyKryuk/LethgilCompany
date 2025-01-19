using Core.View;
using System;
using UnityEngine;

namespace MovementController
{
    public class CharacterAnimatorController : AnimatorController<CharacterControllerAnimatorParameter>
    {
        public event Action<AnimationEvent> Landed;
        public event Action<AnimationEvent> Footstep;

        private void OnLand(AnimationEvent animationEvent)
        {
            Landed?.Invoke(animationEvent);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            Footstep?.Invoke(animationEvent);
        }
    }
}
