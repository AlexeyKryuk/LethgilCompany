using Core.Model;
using Core.View;
using UnityEngine;

namespace Core
{
    public class PlayerPresenter : ISaveLoaded, IPresenter
    {
        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;

        private readonly GameObject _playerView;
        private readonly GameObject _playerCameraView;
        private readonly PlayerConfig _playerConfig;

        private ICharacterView _characterView;
        private Player _model;

        public string Key => "PlayerInfo";

        public PlayerPresenter(PlayerConfig config, ISaveService<Player> saveService, IInputService inputService, 
            GameObject playerView, GameObject playerCameraView)
        {
            _playerConfig = config;
            _saveService = saveService;
            _inputService = inputService;
            _playerView = playerView;
            _playerCameraView = playerCameraView;
        }

        public void Start()
        {
            Initialize();
            LoadModel();
        }

        public void Tick()
        {
            _characterView.Update(_inputService.CharacterInputs);

            _model.Transformable.SetPosition(_characterView.ControllerView.Transform.position);
        }

        public void LateTick()
        {
            _characterView.LateUpdate(_inputService.CameraInputs);
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
        }

        public T GetView<T>()
        {
            return _playerView.GetComponentInChildren<T>();
        }

        private void Initialize()
        {
            ICharacterControllerView controllerView = _playerView.GetComponentInChildren<ICharacterControllerView>();
            ICharacterCameraView cameraView = _playerCameraView.GetComponentInChildren<ICharacterCameraView>();

            controllerView.SetCameraTransform(cameraView.Transform);
            cameraView.SetFollowTransform(controllerView.CameraTarget, controllerView.CameraFollow);

            _characterView = new CharacterView(controllerView, cameraView);
        }

        private void LoadModel()
        {
            Transformable transformable = new Transformable(_playerView.transform);
            Movement movement = new Movement(_playerConfig.TransformSettings.Speed, _playerConfig.TransformSettings.Jumping);
            Damage damage = new Damage(_playerConfig.DamageSettings.Min, _playerConfig.DamageSettings.Max);

            _model = _saveService.Load(this, new Player(transformable, movement, damage));
        }
    }
}
