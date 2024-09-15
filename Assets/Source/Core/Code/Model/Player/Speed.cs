using System;

namespace Core.Model
{
    [Serializable]
    public struct Speed
    {
        public Speed(float walk, float sprint)
        {
            Walk = walk;
            Sprint = sprint;
        }

        public float Walk;
        public float Sprint;
    }
}
