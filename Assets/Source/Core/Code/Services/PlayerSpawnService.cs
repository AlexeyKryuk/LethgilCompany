using Core.Model;
using UnityEngine;

namespace Core
{
    public class PlayerSpawnService
    {
        private readonly PlayerCharacterFactory _factory;
        private readonly PlayerConfig _config;

        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;

        public PlayerSpawnService(PlayerCharacterFactory factory, PlayerConfig config, 
            ISaveService<Player> saveService, IInputService inputService)
        {
            _factory = factory;
            _config = config;
            _saveService = saveService;
            _inputService = inputService;
        }

        public PlayerPresenter Spawn(Vector3 position)
        {
            GameObject player = _factory.Create();
            Camera camera = _factory.CreateCamera();

            IMovementView movementView = player.GetComponent<IMovementView>();

            player.transform.position = position;
            camera.transform.position = position - camera.transform.forward * 5;

            return new PlayerPresenter(_saveService, _inputService, movementView, _config);
        }
    }
}
