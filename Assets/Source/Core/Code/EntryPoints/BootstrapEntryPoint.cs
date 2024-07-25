using UnityEngine;
using VContainer.Unity;

namespace Core
{
    public class BootstrapEntryPoint : IStartable
    {
        private SceneLoadService _sceneLoadService;

        public BootstrapEntryPoint(SceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public void Start()
        {
            Debug.Log("Loaded");
            _sceneLoadService.LoadGameScene();
        }
    }
}
