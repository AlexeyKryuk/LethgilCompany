using System;
using VContainer.Unity;

namespace Core
{
    public interface IPlayerService : ILifetimeCycleService, IInitializable, ITickable, ILateTickable, IDisposable
    {
        T GetView<T>();
    }
}
