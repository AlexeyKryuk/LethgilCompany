using UnityEngine;

namespace Core
{
    public class PlayerSpawnService
    {
        private readonly PlayerCharacterFactory _factory;
        private readonly TransformSettings _settings;

        public PlayerSpawnService(PlayerCharacterFactory factory, PlayerConfig config)
        {
            _factory = factory;
            _settings = config.TransformSettings;

            Debug.Log("Player Spawner Created!");
        }

        public void Spawn(Vector3 position)
        {
            PlayerPresenter player = _factory.Create();
            Camera camera = _factory.CreateCamera();

            IMovementView movementView = player.GetComponent<IMovementView>();

            player.Construct(new PlayerPrefsSaveSystem<Player>(), movementView, _settings);

            player.transform.position = position;
            camera.transform.position = position - camera.transform.forward * 5;
        }
    }
}
