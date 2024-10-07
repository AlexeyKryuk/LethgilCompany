using System;
using VContainer.Unity;

namespace Core
{
    public interface IPlayerService : ILifetimeCycleService, ITickable, ILateTickable, IDisposable
    {
        T GetView<T>();
    }
}
