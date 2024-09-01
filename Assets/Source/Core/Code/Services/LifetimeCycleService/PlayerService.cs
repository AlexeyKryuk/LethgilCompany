using Core.Model;
using Core.View;
using UnityEngine;

namespace Core
{
    public class PlayerService : ILifetimeCycleService
    {
        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;
        private readonly IGrabbingService _grabbingService;

        private readonly PlayerCharacterFactory _factory;
        private readonly PlayerSpawnPoint _spawnPoint;  
        private readonly PlayerConfig _config;

        private PlayerPresenter _presenter;

        public PlayerService(PlayerCharacterFactory factory, PlayerConfig config, PlayerSpawnPoint spawnPoint,
            ISaveService<Player> saveService, IInputService inputService, IGrabbingService grabbingService)
        {
            _factory = factory;
            _config = config;
            _spawnPoint = spawnPoint;
            _saveService = saveService;
            _inputService = inputService;
            _grabbingService = grabbingService;
        }

        public void Start()
        {
            _presenter = Spawn();
            _presenter.Start();
        }

        public void Tick()
        {
            _presenter.Tick();
        }

        public void LateTick()
        {
            _presenter.LateTick();
        }

        public void Dispose()
        {
            _presenter.Dispose();
        }

        private PlayerPresenter Spawn()
        {
            _factory.CreateMainCamera();

            var player = _factory.Create(_spawnPoint.transform.position, Quaternion.identity);
            var playerCamera = _factory.CreatePlayerCamera();

            return new PlayerPresenter(_config, _saveService, _inputService, _grabbingService, player, playerCamera);
        }
    }
}
