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
            _factory.CreateMainCamera();

            var player = _factory.Create(position, Quaternion.identity);
            var playerCamera = _factory.CreatePlayerCamera();

            ICharacterControllerView controllerView = player.GetComponentInChildren<ICharacterControllerView>();
            ICharacterCameraView cameraView = playerCamera.GetComponent<ICharacterCameraView>();

            PrepareCamera(controllerView, cameraView);
            PrepareCharacter(controllerView, position, playerCamera.transform);

            return new PlayerPresenter(_saveService, _inputService, controllerView, cameraView, _config);
        }

        private void PrepareCharacter(ICharacterControllerView controllerView, Vector3 position, Transform camera)
        {
            controllerView.Transform.position = position;
            controllerView.SpecifyCameraTransform(camera);
        }

        private void PrepareCamera(ICharacterControllerView player, ICharacterCameraView cameraView)
        {
            cameraView.SetFollowTransform(player.CameraTarget, player.CameraFollow);
        }
    }
}
