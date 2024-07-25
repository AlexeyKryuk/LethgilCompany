using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Movement
    {
        [SerializeField] private Speed _speed;
        [SerializeField] private Jumping _jumping;

        public Movement(Speed speed, Jumping jumping)
        {
            _speed = speed;
            _jumping = jumping;
        }

        public Speed Speed => _speed;
        public Jumping Jumping => _jumping;

        public void BuffSpeed(Speed speed)
        {
            float walk = Math.Max(0, _speed.Walk + speed.Walk);
            float running = Math.Max(0, _speed.Running + speed.Running);
            float sprint = Math.Max(0, _speed.Sprint + speed.Sprint);

            _speed = new Speed(walk, running, sprint);
        }

        public void BuffJump(Jumping jumping)
        {
            float gravity = Math.Max(0, _jumping.Gravity + jumping.Gravity);
            float height = Math.Max(0, _jumping.Height + jumping.Height);
            float time = Math.Max(0, _jumping.Time + jumping.Time);

            _jumping = new Jumping(gravity, height, time);
        }
    }
}
