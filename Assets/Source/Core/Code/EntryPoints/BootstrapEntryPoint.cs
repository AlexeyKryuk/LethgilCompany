using VContainer.Unity;

namespace Core
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly SceneLoadService _sceneLoadService;

        public BootstrapEntryPoint(SceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _sceneLoadService.LoadGameScene();
        }
    }
}
