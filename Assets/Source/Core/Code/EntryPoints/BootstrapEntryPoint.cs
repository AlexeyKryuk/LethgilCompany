using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core
{
    public class BootstrapEntryPoint : IInitializable, IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly IList<ILifetimeCycleService> _services;
        private readonly SceneLoadService _sceneLoadService;

        public BootstrapEntryPoint(SceneLoadService sceneLoadService, IUIService uiService)
        {
            _sceneLoadService = sceneLoadService;

            _services = new List<ILifetimeCycleService>()
            {
                uiService,
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

            _sceneLoadService.LoadGameScene();
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
