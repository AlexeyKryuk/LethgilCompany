using Core.Model;
using Core.View;
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
            var player = _factory.Create(position, Quaternion.identity);
            var playerCamera = _factory.CreatePlayerCamera();
            var mainCamera = _factory.CreateMainCamera();

            ICharacterControllerView controllerView = player.GetComponent<ICharacterControllerView>();
            ICharacterCameraView cameraView = playerCamera.GetComponent<ICharacterCameraView>();

            PrepareCamera(player, cameraView);
            PrepareCharacter(controllerView, position, mainCamera.transform);

            return new PlayerPresenter(_saveService, _inputService, controllerView, cameraView, _config);
        }

        private void PrepareCharacter(ICharacterControllerView controllerView, Vector3 position, Transform mainCamera)
        {
            controllerView.Transform.position = position;
            controllerView.SpecifyCameraTransform(mainCamera);
        }

        private void PrepareCamera(GameObject player, ICharacterCameraView cameraView)
        {
            cameraView.SetFollowTransform(player.transform);
        }
    }
}
