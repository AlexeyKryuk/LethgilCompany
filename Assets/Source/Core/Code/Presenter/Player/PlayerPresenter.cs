using Core.Model;
using Core.View;
using System;
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

        public void Initialize()
        {
            LoadModel();

            var controllerView = GetView<ICharacterControllerView>();
            var cameraView = GetView<ICharacterCameraView>();

            InitializeCharacterController(controllerView, cameraView);
            cameraView.SetFollowTransform(controllerView.CameraTarget, controllerView.CameraFollow);

            _characterView = new CharacterView(controllerView, cameraView);
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
            T characterview = _playerView.GetComponentInChildren<T>();

            if (characterview == null)
                characterview = _playerCameraView.GetComponentInChildren<T>();

            if (characterview == null)
                throw new NullReferenceException();

            return characterview;
        }

        private void LoadModel()
        {
            Transformable transformable = new Transformable(_playerView.transform);
            Movement movement = new Movement(_playerConfig.TransformSettings.Speed, _playerConfig.TransformSettings.Jumping);
            Damage damage = new Damage(_playerConfig.DamageSettings.Min, _playerConfig.DamageSettings.Max);

            _model = _saveService.Load(this, new Player(transformable, movement, damage));
        }

        private void InitializeCharacterController(ICharacterControllerView character, ICharacterCameraView camera)
        {
            character.SetCameraTransform(camera.Transform);
            character.Initialize(new TransformSettings(_model.Movement.Speed, _model.Movement.Jumping));

            character.Transform.position = _model.Transformable.Position;
            character.Transform.rotation = _model.Transformable.Rotation;
            character.Transform.localScale = _model.Transformable.Scale;
        }
    }
}
