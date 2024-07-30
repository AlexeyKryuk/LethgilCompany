using Core.Model;
using Core.View;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using VContainer.Unity;

namespace Core
{
    public class PlayerPresenter : ISaveLoaded, IStartable, ITickable, ILateTickable, IDisposable
    {
        private ISaveService<Player> _saveService;
        private IInputService _inputService;
        private ICharacterControllerView _controllerView;
        private ICharacterCameraView _cameraView;

        private PlayerConfig _playerConfig;
        private Player _model;

        public string Key => "PlayerInfo";

        public PlayerPresenter(ISaveService<Player> saveService, IInputService inputService, 
            ICharacterControllerView controllerView, ICharacterCameraView cameraView, PlayerConfig config)
        {
            _saveService = saveService;
            _inputService = inputService;
            _controllerView = controllerView;
            _cameraView = cameraView;
            _playerConfig = config;
        }

        private void LoadSaves(ICharacterControllerView controllerView, PlayerConfig config)
        {
            Transform transform = controllerView.Transform;

            Transformable transformable = new Transformable(transform.position, transform.localScale, transform.rotation);
            Movement movement = new Movement(config.TransformSettings.Speed, config.TransformSettings.Jumping);
            Damage damage = new Damage(config.DamageSettings.Min, config.DamageSettings.Max);

            _model = _saveService.Load(this, new Player(transformable, movement, damage));

            controllerView.Transform.position = _model.Transformable.Position;
        }

        public void Start()
        {
            LoadSaves(_controllerView, _playerConfig);
        }

        public void Tick()
        {
            _controllerView.UpdateInputs(_inputService.CharacterInputs, _cameraView.Transform.rotation);
            _model.Transformable.SetPosition(_controllerView.Transform.position);
        }

        public void LateTick()
        {
            _cameraView.UpdateInput(_inputService.CameraInputs);
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
            Debug.Log(_model.Transformable.Position);

        }
    }
}
