using System;

namespace CharacterController
{
    [Serializable]
    public struct ControllerSettings
    {
        public Speed Speed;
        public Jumping Jumping;

        public ControllerSettings(Speed speed, Jumping jumping)
        {
            Speed = speed;
            Jumping = jumping;
        }
    }
}
