using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IInitializable, IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly IList<IInitializable> _initializables = new List<IInitializable>();
        private readonly IList<IStartable> _startables = new List<IStartable>();
        private readonly IList<ITickable> _tickables = new List<ITickable>();
        private readonly IList<ILateTickable> _lateTickables = new List<ILateTickable>();
        private readonly IList<IDisposable> _disposables = new List<IDisposable>();

        public GameEntryPoint(IReadOnlyList<ILifetimeCycleService> services)
        {
            foreach (var service in services)
            {
                if (service is IInitializable initializable)
                    _initializables.Add(initializable);

                if (service is IStartable startable)
                    _startables.Add(startable);

                if (service is ITickable tickable)
                    _tickables.Add(tickable);

                if (service is ILateTickable lateTickable)
                    _lateTickables.Add(lateTickable);

                if (service is IDisposable disposable)
                    _disposables.Add(disposable);
            }
        }

        public void Initialize()
        {
            foreach (var service in _initializables)
                service.Initialize();
        }

        public void Start()
        {
            foreach (var service in _startables)
                service.Start();
        }

        public void Tick()
        {
            foreach (var service in _tickables)
                service.Tick();
        }

        public void LateTick()
        {
            foreach (var service in _lateTickables)
                service.LateTick();
        }

        public void Dispose()
        {
            foreach (var service in _disposables)
                service.Dispose();
        }
    }
}
