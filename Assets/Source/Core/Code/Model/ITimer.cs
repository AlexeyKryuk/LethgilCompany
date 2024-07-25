namespace Core
{
    public interface ITimer
    {
        bool IsOver { get; }
        bool IsStarted { get; }
        void Start();
        void Tick(float deltaTime);
    }
}
