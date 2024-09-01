using VContainer.Unity;

namespace Core
{
    public interface IGrabbingService : ITickable
    {
        void Initialize(IGrabberView grabber);
    }
}
