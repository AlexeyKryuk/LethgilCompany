using Core.View;
using UnityEngine;

namespace Core
{
    public class PlayerCharacterInputs : ICharacterInputs
    {
        public PlayerCharacterInputs()
        {
            ActionButton = new Button();
        }

        public Vector2 MoveAxis { get; set; }
        public bool JumpDown { get; set; }
        public bool Sprint { get; set; }
        public Button ActionButton { get; set; }
    }
}
