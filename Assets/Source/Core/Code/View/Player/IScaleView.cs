using System.Collections;

namespace Core
{
    public interface IScaleView
    {
        public IEnumerator Scale(float deltaTime);
        public IEnumerator Unscale(float deltaTime);
    }
}
