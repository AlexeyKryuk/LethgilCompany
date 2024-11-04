namespace Core.View
{
    public interface ICharacterView
    {
        ICharacterControllerView ControllerView { get; }
        ICharacterCameraView CameraView { get; }

        void Start();
        void LateUpdate(ICameraInputs inputs);
        void Update(ICharacterInputs inputs);
    }
}
