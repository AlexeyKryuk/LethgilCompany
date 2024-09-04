namespace Core
{
    public interface ILifetimeCycleService
    {
        void Initialize();
        void Start();
        void Tick();
        void LateTick();
        void Dispose();
    }
}
