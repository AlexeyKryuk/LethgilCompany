using System;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly ILifetimeCycleService _playerService;

        public GameEntryPoint(PlayerService playerService)
        {
            _playerService = playerService;
        }

        public void Start()
        {
            _playerService.Start();
        }

        public void Tick()
        {
            _playerService.Tick();
        }

        public void LateTick()
        {
            _playerService.LateTick();
        }

        public void Dispose()
        {
            _playerService.Dispose();
        }
    }
}
