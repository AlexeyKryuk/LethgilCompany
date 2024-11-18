using CharacterController;
using UnityEngine;

namespace Combat
{
    public interface ICharacterCombatView
    {
        Transform Transform { get; }

        void Start();
        void Initialize(ICharacterControllerView characterControllerView);
        void UpdateInputs(PlayerCharacterInputs inputs);
    }
}
