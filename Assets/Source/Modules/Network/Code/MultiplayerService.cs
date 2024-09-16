using Core;
using Core.Model;
using UnityEngine;

namespace Network
{
    public class MultiplayerService : IPlayerService
    {
        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;

        private readonly PlayerCharacterFactory _factory;
        private readonly PlayerSpawnPoint _spawnPoint;
        private readonly PlayerConfig _config;

        private PlayerPresenter _presenter;

        public MultiplayerService(PlayerCharacterFactory factory, PlayerConfig config, PlayerSpawnPoint spawnPoint,
            ISaveService<Player> saveService, IInputService inputService)
        {
            _factory = factory;
            _config = config;
            _spawnPoint = spawnPoint;
            _saveService = saveService;
            _inputService = inputService;
        }

        public IPresenter Presenter => _presenter;

        public void Initialize()
        {
            _presenter = Spawn();
            _presenter.Initialize();
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

            return new PlayerPresenter(_config, _saveService, _inputService, player, playerCamera);
        }

        public void Start() { }
    }
}
