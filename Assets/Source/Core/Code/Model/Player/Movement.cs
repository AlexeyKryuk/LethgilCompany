using System;

namespace Core.Model
{
    [Serializable]
    public class Movement
    {
        public Movement(Speed speed, Jumping jumping)
        {
            Speed = speed;
            Jumping = jumping;
        }

        public Speed Speed;
        public Jumping Jumping;

        public void BuffSpeed(Speed speed)
        {
            float walk = Math.Max(0, Speed.Walk + speed.Walk);
            float sprint = Math.Max(0, Speed.Sprint + speed.Sprint);

            Speed = new Speed(walk, sprint);
        }

        public void BuffJump(Jumping jumping)
        {
            float gravity = Math.Max(0, Jumping.Gravity + jumping.Gravity);
            float height = Math.Max(0, Jumping.Height + jumping.Height);
            float timeOut = Math.Max(0, Jumping.Timeout + jumping.Timeout);
            float fallTimeOut = Math.Max(0, Jumping.FallTimeout + jumping.FallTimeout);

            Jumping = new Jumping(gravity, height, timeOut, fallTimeOut);
        }
    }
}
