using Core.Model;
using Core.View;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
            Cursor.lockState = CursorLockMode.Locked; // Вынести это отсюда

            var player = _factory.Create(position, Quaternion.identity);
            var camera = _factory.CreateCamera();

            ICharacterControllerView controllerView = player.GetComponent<ICharacterControllerView>();
            ICharacterCameraView cameraView = camera.GetComponent<ICharacterCameraView>();

            PrepareCamera(player, controllerView, cameraView);
            SetCharacterPosition(position, controllerView);

            return new PlayerPresenter(_saveService, _inputService, controllerView, cameraView, _config);
        }

        private void SetCharacterPosition(Vector3 position, ICharacterControllerView controllerView)
        {
            controllerView.Transform.position = position;
        }

        private void PrepareCamera(GameObject player, ICharacterControllerView controllerView, ICharacterCameraView cameraView)
        {
            cameraView.SetFollowTransform(controllerView.CameraFollowPoint);

            cameraView.IgnoredColliders.Clear();
            cameraView.IgnoredColliders.AddRange(player.GetComponentsInChildren<Collider>());
        }
    }
}
