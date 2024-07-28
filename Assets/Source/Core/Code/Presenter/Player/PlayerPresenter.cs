using Core.Model;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class PlayerPresenter : ISaveLoaded, IStartable, ITickable, IDisposable
    {
        private ISaveService<Player> _saveService;
        private IInputService _inputService;
        private IMovementView _movementView;

        private PlayerConfig _playerConfig;
        private Player _model;

        public string Key => "PlayerInfo";

        public PlayerPresenter(ISaveService<Player> saveService, IInputService inputService, IMovementView movementView, PlayerConfig config)
        {
            _saveService = saveService;
            _inputService = inputService;
            _movementView = movementView;
            _playerConfig = config;
        }

        private void LoadSaves(IMovementView movementView, PlayerConfig config)
        {
            Transformable transformable = new Transformable(movementView.Position, movementView.Scale, movementView.Rotation);
            Movement movement = new Movement(config.TransformSettings.Speed, config.TransformSettings.Jumping);
            Damage damage = new Damage(config.DamageSettings.Min, config.DamageSettings.Max);
            Player defaultModel = new Player(transformable, movement, damage);

            _model = _saveService.Load(this, defaultModel);
        }

        public void Start()
        {
            Debug.Log("Start");

            LoadSaves(_movementView, _playerConfig);
        }

        public void Tick()
        {
            Debug.Log(_inputService.Move);

            Vector3 newPosition = _movementView.MoveAt(_inputService.Move);
            _model.Transformable.SetPosition(newPosition);
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
        }
    }
}
