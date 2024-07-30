using Core.View;
using System;
using UnityEngine;

namespace Core
{
    public interface IInputService
    {
        ICharacterInputs CharacterInputs { get; }
        ICameraInputs CameraInputs { get; }

        event Action Jump;
    }
}
