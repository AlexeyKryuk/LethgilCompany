using Core;
using Core.View;
using System;
using UnityEngine;
using VContainer.Unity;

namespace CharacterController
{
    public class CharacterControllerPresenter : ISaveLoaded, ILifetimeCycleService, IStartable, ITickable, ILateTickable, IDisposable
    {
        private readonly IPlayerPresenter _playerPresenter;
        private readonly ISaveService<ControllerData> _saveService;
        private readonly IInputService<PlayerCharacterInputs> _characterInput;
        private readonly IInputService<PlayerCameraInputs> _cameraInput;
        private readonly ControllerStaticData _staticData;

        private ICharacterControllerView _controllerView;
        private ICharacterCameraView _cameraView;

        private ControllerData _model;

        public string Key => nameof(ControllerData);

        public CharacterControllerPresenter(IInputService<PlayerCharacterInputs> characterInput, IInputService<PlayerCameraInputs> cameraInput,
            IPlayerPresenter playerPresenter, ISaveService<ControllerData> saveService, ControllerStaticData staticData)
        {
            _playerPresenter = playerPresenter;
            _saveService = saveService;
            _characterInput = characterInput;
            _cameraInput = cameraInput;
            _staticData = staticData;
        }

        public void Start()
        {
            _controllerView = _playerPresenter.GetView<ICharacterControllerView>();
            _cameraView = _playerPresenter.GetView<ICharacterCameraView>();

            _model = LoadModel(_controllerView.Transform, _staticData.ControllerSettings);

            _cameraView.SetFollowTransform(_controllerView.CameraTarget, _controllerView.CameraFollow);
            _controllerView.Initialize(_model.ControllerSettings, _cameraView.Transform);
        }

        public void Tick()
        {
            _controllerView.UpdateInputs(_characterInput.Inputs);
            _model.Position = _controllerView.Transform.position;
        }

        public void LateTick()
        {
            _cameraView.UpdateInput(_cameraInput.Inputs);
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
        }

        private ControllerData LoadModel(Transform transform, ControllerSettings controllerSettings)
            => _saveService.Load(this, new ControllerData(transform.position, transform.rotation, transform.localScale, controllerSettings));
    }
}
