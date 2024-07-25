using System.Collections;

namespace Core
{
    public interface ICoroutineHandler
    {
        public void StartCoroutine(IEnumerator routine);
    }
}
