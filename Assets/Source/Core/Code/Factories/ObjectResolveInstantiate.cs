using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ObjectResolveInstantiate : IInstantiatable
    {
        private readonly IObjectResolver _resolver;

        public ObjectResolveInstantiate(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
            => _resolver.Instantiate(prefab, position, rotation);
    }
}
