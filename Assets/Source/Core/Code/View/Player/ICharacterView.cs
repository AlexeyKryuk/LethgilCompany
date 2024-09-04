namespace Core.View
{
    public interface ICharacterView
    {
        ICharacterControllerView ControllerView { get; }
        ICharacterCameraView CameraView { get; }

        void LateUpdate(ICameraInputs inputs);
        void Update(ICharacterInputs inputs);
    }
}
