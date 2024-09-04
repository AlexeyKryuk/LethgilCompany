namespace Core.View
{
    public class CharacterView : ICharacterView
    {
        private readonly ICharacterControllerView _controllerView;
        private readonly ICharacterCameraView _cameraView;

        public CharacterView(ICharacterControllerView controllerView, ICharacterCameraView cameraView)
        {
            _controllerView = controllerView;
            _cameraView = cameraView;
        }

        public ICharacterControllerView ControllerView => _controllerView;
        public ICharacterCameraView CameraView => _cameraView;

        public void Update(ICharacterInputs inputs)
        {
            _controllerView.UpdateInputs(inputs);
        }

        public void LateUpdate(ICameraInputs inputs)
        {
            _cameraView.UpdateInput(inputs);
        }
    }
}
