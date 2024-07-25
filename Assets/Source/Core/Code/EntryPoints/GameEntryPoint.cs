using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameEntryPoint : IStartable
    {
        private PlayerSpawnPoint _playerSpawnPoint;
        private PlayerSpawnService _playerSpawnService;

        public GameEntryPoint(PlayerSpawnService playerSpawnService, PlayerSpawnPoint playerSpawnPoint)
        {
            _playerSpawnService = playerSpawnService;
            _playerSpawnPoint = playerSpawnPoint;
        }

        public void Start()
        {
            _playerSpawnService.Spawn(_playerSpawnPoint.transform.position);
        }
    }
}
