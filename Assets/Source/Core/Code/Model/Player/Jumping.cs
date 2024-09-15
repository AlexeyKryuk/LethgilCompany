using System;

namespace Core.Model
{
    [Serializable]
    public struct Jumping
    {
        public Jumping(float gravity, float height, float timeOut, float fallTimeout)
        {
            Gravity = gravity;
            Height = height;
            Timeout = timeOut;
            FallTimeout = fallTimeout;
        }

        public float Gravity;
        public float Height;
        public float Timeout;
        public float FallTimeout;
    }
}
