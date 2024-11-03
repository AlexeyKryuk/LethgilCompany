using Core.Model;
using UnityEngine;

namespace Core
{
    public class PlayerService : IPlayerService
    {
        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;
        private readonly IPlayerCharacterFactory _factory;
        private readonly PlayerSpawnPoint _spawnPoint;
        private readonly PlayerConfig _config;

        private PlayerPresenter _presenter;

        public PlayerService(IPlayerCharacterFactory factory, PlayerConfig config, PlayerSpawnPoint spawnPoint,
            ISaveService<Player> saveService, IInputService inputService)
        {
            _factory = factory;
            _config = config;
            _spawnPoint = spawnPoint;
            _saveService = saveService;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _factory.CreateMainCamera();

            var player = _factory.CreateCharacter(_spawnPoint.transform.position, Quaternion.identity);
            var playerCamera = _factory.CreatePlayerCamera();

            _presenter = new PlayerPresenter(_config, _saveService, _inputService, player, playerCamera);
            _presenter.Initialize();
        }

        public void Tick() => _presenter.Tick();
        public void LateTick() => _presenter.LateTick();
        public void Dispose() => _presenter.Dispose();

        public T GetView<T>() => _presenter.GetView<T>();
    }
}
