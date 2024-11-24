using CharacterController;
using Core;
using System;
using VContainer.Unity;

namespace Combat
{
    public class CharacterCombatPresenter : ISaveLoaded, ILifetimeCycleService, IStartable, ITickable, IDisposable
    {
        private readonly IPlayerPresenter _playerPresenter;
        private readonly ISaveService<CombatData> _saveService;
        private readonly IInputService<PlayerCharacterInputs> _characterInput;
        private readonly CombatStaticData _staticData;

        private ICharacterCombatView _combatView;
        private ICharacterControllerView _controllerView;

        private CombatData _model;

        public CharacterCombatPresenter(IPlayerPresenter playerPresenter, ISaveService<CombatData> saveService, 
            IInputService<PlayerCharacterInputs> characterInput, CombatStaticData staticData)
        {
            _playerPresenter = playerPresenter;
            _saveService = saveService;
            _characterInput = characterInput;
            _staticData = staticData;
        }

        public string Key => nameof(CombatData);

        public void Start()
        {
            _model = LoadModel(_staticData.DamageSettings);

            _combatView = _playerPresenter.GetView<ICharacterCombatView>();
            _controllerView = _playerPresenter.GetView<ICharacterControllerView>();

            _combatView.Initialize(_controllerView);
            _combatView.Start();
        }

        public void Tick()
        {
            _combatView.UpdateInputs(_characterInput.Inputs);
        }

        public void Dispose()
        {
            _saveService.Save(this, _model);
        }

        private CombatData LoadModel(DamageSettings damageSettings)
            => _saveService.Load(this, new CombatData(damageSettings));
    }
}
