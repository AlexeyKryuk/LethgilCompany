using UnityEngine;

namespace Core.View
{
    public interface ICharacterCombatView
    {
        Transform Transform { get; }

        void Initialize(ICharacterControllerView characterControllerView);
        void Start();
        void UpdateInputs(ICharacterInputs inputs);
    }
}
