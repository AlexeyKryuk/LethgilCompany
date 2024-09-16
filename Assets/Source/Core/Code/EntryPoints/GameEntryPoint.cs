using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IInitializable, IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly IList<ILifetimeCycleService> _services;

        public GameEntryPoint(IPlayerService playerService, IGrabbingService grabbingService)
        {
            _services = new List<ILifetimeCycleService>()
            {
                playerService,
                grabbingService
            };
        }

        public void Initialize()
        {
            foreach (var service in _services)
                service.Initialize();
        }

        public void Start()
        {
            foreach (var service in _services)
                service.Start();
        }

        public void Tick()
        {
            foreach (var service in _services)
                service.Tick();
        }

        public void LateTick()
        {
            foreach (var service in _services)
                service.LateTick();
        }

        public void Dispose()
        {
            foreach (var service in _services)
                service.Dispose();
        }
    }
}
