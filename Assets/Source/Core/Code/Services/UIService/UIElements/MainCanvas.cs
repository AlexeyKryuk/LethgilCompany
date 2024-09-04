namespace Core
{
    public class MainCanvas : BaseUIElement
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
