namespace Core.Model
{
    public struct Jumping
    {
        public Jumping(float gravity, float height, float time)
        {
            Gravity = gravity;
            Height = height;
            Time = time;
        }

        public float Gravity;
        public float Height;
        public float Time;
    }
}
