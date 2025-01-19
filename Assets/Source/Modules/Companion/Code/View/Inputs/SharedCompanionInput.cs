using BehaviorDesigner.Runtime;

namespace Companion
{
    public class SharedCompanionInput : SharedVariable<CompanionInput>
    {
        public static implicit operator SharedCompanionInput(CompanionInput value) 
            => new SharedCompanionInput { Value = value };
    }
}
