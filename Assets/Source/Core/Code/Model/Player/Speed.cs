using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct Speed
    {
        public Speed(float walk, float running, float sprint)
        {
            Walk = walk;
            Running = running;
            Sprint = sprint;
        }

        [field: SerializeField] public float Walk { get; private set; }
        [field: SerializeField] public float Running { get; private set; }
        [field: SerializeField] public float Sprint { get; private set; }
    }
}
