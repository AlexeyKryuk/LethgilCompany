using Core;
using System;
using VContainer.Unity;

namespace ItemGrabbing
{
    public interface IGrabbingService : ILifetimeCycleService, IInitializable, IStartable, ITickable, IDisposable
    {

    }
}
