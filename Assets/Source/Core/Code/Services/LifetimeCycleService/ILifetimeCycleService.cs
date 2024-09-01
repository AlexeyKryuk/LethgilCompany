namespace Core
{
    public interface ILifetimeCycleService
    {
        void Start();
        void Tick();
        void LateTick();
        void Dispose();
    }
}
