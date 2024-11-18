using Core;
using UnityEngine;

namespace CharacterController
{
    public struct PlayerCharacterInputs
    {
        public Vector2 MoveAxis;
        public bool JumpDown;
        public bool LMB_Down;
        public bool Sprint;
        public Button ActionButton;

        public PlayerCharacterInputs(Button actionButton) : this()
        {
            ActionButton = actionButton;
        }
    }
}
