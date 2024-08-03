using System;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IStartable, ITickable, ILateTickable, IDisposable
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
            Cursor.lockState = CursorLockMode.Locked; // Вынести это отсюда

            _player = _playerSpawnService.Spawn(_playerSpawnPoint.transform.position);
            _player.Start();
        }

        public void Tick()
        {
            _player.Tick();
        }

        public void LateTick()
        {
            _player.LateTick();
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }
}
