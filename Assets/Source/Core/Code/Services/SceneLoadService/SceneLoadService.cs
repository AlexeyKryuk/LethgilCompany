using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoadService
    {
        private static string GameScene = "PolygonCity";

        public AsyncOperation LoadGameScene()
        {
            return SceneManager.LoadSceneAsync(GameScene);
        }
    }
}
