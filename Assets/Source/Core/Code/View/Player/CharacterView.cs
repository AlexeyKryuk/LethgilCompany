namespace Core.View
{
    public class CharacterView : ICharacterView
    {
        private readonly ICharacterControllerView _controllerView;
        private readonly ICharacterCameraView _cameraView;
        private readonly ICharacterCombatView _combatView;

        public CharacterView(ICharacterControllerView controllerView, ICharacterCameraView cameraView, ICharacterCombatView combatView)
        {
            _controllerView = controllerView;
            _cameraView = cameraView;
            _combatView = combatView;
        }

        public ICharacterControllerView ControllerView => _controllerView;
        public ICharacterCameraView CameraView => _cameraView;
        public ICharacterCombatView CombatView => _combatView;

        public void Start()
        {
            _combatView.Start();
        }

        public void Update(ICharacterInputs inputs)
        {
            _controllerView.UpdateInputs(inputs);
            _combatView.UpdateInputs(inputs);
        }

        public void LateUpdate(ICameraInputs inputs)
        {
            _cameraView.UpdateInput(inputs);
        }
    }
}
