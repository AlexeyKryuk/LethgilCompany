using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Companion
{
    public class SetRandomDirection : Action
    {
        public Vector2 Direction;

        public override TaskStatus OnUpdate()
        {
            Direction = Random.insideUnitCircle.normalized;
            return TaskStatus.Success;
        }
    }
}
