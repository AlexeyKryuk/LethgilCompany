using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoadService
    {
        private readonly string _gameScene;

        public SceneLoadService(string gameScene)
        {
            _gameScene = gameScene;
        }

        public AsyncOperation LoadGameScene()
        {
            return SceneManager.LoadSceneAsync(_gameScene);
        }
    }
}
