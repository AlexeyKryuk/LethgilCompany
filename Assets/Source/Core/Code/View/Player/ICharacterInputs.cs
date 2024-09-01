using UnityEngine;

namespace Core.View
{
    public interface ICharacterInputs
    {
        public Vector2 MoveAxis { get; set; }
        public bool JumpDown { get; set; }
        public bool Sprint { get; set; }
        public bool ActionButton { get; set; }
    }
}
