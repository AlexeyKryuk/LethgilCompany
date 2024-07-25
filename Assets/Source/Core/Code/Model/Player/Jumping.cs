using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct Jumping
    {
        public Jumping(float gravity, float height, float time)
        {
            Gravity = gravity;
            Height = height;
            Time = time;
        }

        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float Height { get; private set; }
        [field: SerializeField] public float Time { get; private set; }
    }
}
