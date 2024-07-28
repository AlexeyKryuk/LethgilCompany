using System;
using UnityEngine;

namespace Core
{
    public interface IInputService
    {
        Vector2 Move { get; }
        Vector2 Look { get; }

        event Action Jump;
    }
}
