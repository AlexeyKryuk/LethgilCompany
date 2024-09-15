using System;

namespace Core.Model
{
    [Serializable]
    public struct TransformSettings
    {
        public Speed Speed;
        public Jumping Jumping;

        public TransformSettings(Speed speed, Jumping jumping)
        {
            Speed = speed;
            Jumping = jumping;
        }
    }
}
