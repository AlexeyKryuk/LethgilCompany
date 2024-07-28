using Core.Model;
using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IStartable, ITickable, IDisposable
    {
        private PlayerSpawnPoint _playerSpawnPoint;
        private PlayerSpawnService _playerSpawnService;

        private PlayerPresenter _player;

        public GameEntryPoint(PlayerSpawnService playerSpawnService, PlayerSpawnPoint playerSpawnPoint)
        {
            _playerSpawnService = playerSpawnService;
            _playerSpawnPoint = playerSpawnPoint;
        }

        public void Start()
        {
            _playerSpawnService.Spawn(_playerSpawnPoint.transform.position);
            _player.Start();
        }

        public void Tick()
        {
            _player.Tick();
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }
}
