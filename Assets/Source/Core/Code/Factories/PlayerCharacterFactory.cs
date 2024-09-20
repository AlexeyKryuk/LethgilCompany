using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class PlayerCharacterFactory : IPlayerCharacterFactory
    {
        private readonly GameObject _prefab;
        private readonly GameObject _playerCameraPrefab;
        private readonly GameObject _mainCameraPrefab;
        
        private readonly IObjectResolver _objectResolver;

        public PlayerCharacterFactory(IObjectResolver objectResolver, PlayerConfig config)
        {
            _objectResolver = objectResolver;
            _prefab = config.PlayerPrefab;
            _playerCameraPrefab = config.PlayerCameraPrefab;
            _mainCameraPrefab = config.MainCameraPrefab;
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
            => _objectResolver.Instantiate(_prefab, position, rotation);

        public GameObject CreatePlayerCamera()
            => _objectResolver.Instantiate(_playerCameraPrefab);

        public GameObject CreateMainCamera()
            => _objectResolver.Instantiate(_mainCameraPrefab);
    }
}
