using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class PlayerCharacterFactory
    {
        private readonly GameObject _prefab;
        private readonly Camera _cameraPrefab;
        private readonly IObjectResolver _objectResolver;

        public PlayerCharacterFactory(IObjectResolver objectResolver, PlayerConfig config)
        {
            _objectResolver = objectResolver;
            _prefab = config.Prefab;
            _cameraPrefab = config.CameraPrefab;

            Debug.Log("Factory Created!");
        }

        public GameObject Create()
            => _objectResolver.Instantiate(_prefab);

        public Camera CreateCamera()
            => _objectResolver.Instantiate(_cameraPrefab);
    }
}
