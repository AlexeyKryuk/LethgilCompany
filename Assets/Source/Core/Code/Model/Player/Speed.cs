namespace Core.Model
{
    public struct Speed
    {
        public Speed(float walk, float running, float sprint)
        {
            Walk = walk;
            Running = running;
            Sprint = sprint;
        }

        public float Walk;
        public float Running;
        public float Sprint;
    }
}
