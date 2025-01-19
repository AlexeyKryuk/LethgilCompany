using Core;
using UnityEngine;

namespace Companion
{
    public class CompanionInput : MonoBehaviour, IInputService<MovementInputs>
    {
        public MovementInputs Inputs { get; set; }
    }
}
