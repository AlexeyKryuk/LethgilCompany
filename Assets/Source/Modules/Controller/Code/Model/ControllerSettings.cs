using System;

namespace MovementController
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
