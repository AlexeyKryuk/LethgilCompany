using Core.Model;
using Core.View;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public interface ICharacterView : ITickable, ILateTickable
    {
        ICharacterControllerView ControllerView { get; }
        ICharacterCameraView CameraView { get; }
        IGrabberView GrabberView { get; }
    }

    public class CharacterView : ICharacterView
    {
        private readonly IInputService _inputService;
        private readonly ICharacterControllerView _controllerView;
        private readonly ICharacterCameraView _cameraView;
        private readonly IGrabberView _grabberView;

        public CharacterView(IInputService inputService, ICharacterControllerView controllerView, 
            ICharacterCameraView cameraView, IGrabberView grabberView)
        {
            _inputService = inputService;
            _controllerView = controllerView;
            _cameraView = cameraView;
            _grabberView = grabberView;
        }

        public ICharacterControllerView ControllerView => _controllerView;
        public ICharacterCameraView CameraView => _cameraView;
        public IGrabberView GrabberView => _grabberView;

        public void Tick()
        {
            _controllerView.UpdateInputs(_inputService.CharacterInputs);
        }

        public void LateTick()
        {
            _cameraView.UpdateInput(_inputService.CameraInputs);
        }
    }

    public class PlayerPresenter : ISaveLoaded
    {
        private readonly IInputService _inputService;
        private readonly ISaveService<Player> _saveService;
        private readonly IGrabbingService _grabbingService;

        private readonly GameObject _playerView;
        private readonly GameObject _playerCameraView;
        private readonly PlayerConfig _playerConfig;

        private ICharacterView _characterView;
        private Player _model;

        public string Key => "PlayerInfo";

        public PlayerPresenter(PlayerConfig config, ISaveService<Player> saveService, IInputService inputService, 
            IGrabbingService grabbingService, GameObject playerView, GameObject playerCameraView)
        {
            _playerConfig = config;
            _saveService = saveService;
            _inputService = inputService;
            _grabbingService = grabbingService;
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
            _characterView.Tick();
            _grabbingService.Tick();

            Debug.Log(_model);
            Debug.Log(_characterView);
            _model.Transformable.SetPosition(_characterView.ControllerView.Transform.position);
        }

        public void LateTick()
        {
            _characterView.LateTick();
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
        }

        private void Initialize()
        {
            ICharacterControllerView controllerView = _playerView.GetComponentInChildren<ICharacterControllerView>();
            ICharacterCameraView cameraView = _playerCameraView.GetComponentInChildren<ICharacterCameraView>();
            IGrabberView grabberView = _playerView.GetComponentInChildren<IGrabberView>();

            controllerView.SetCameraTransform(cameraView.Transform);
            cameraView.SetFollowTransform(controllerView.CameraTarget, controllerView.CameraFollow);

            _grabbingService.Initialize(grabberView);

            _characterView = new CharacterView(_inputService, controllerView, cameraView, grabberView);
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
