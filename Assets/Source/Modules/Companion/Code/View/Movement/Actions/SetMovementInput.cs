using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Companion
{
    public class SetMovementInput : Action
    {
        public SharedCompanionInput SelfBotInput;
        public Vector2 Direction;

        public override TaskStatus OnUpdate()
        {
            SelfBotInput.Value.Inputs = new MovementInputs(Direction);
            return TaskStatus.Success;
        }
    }
}
