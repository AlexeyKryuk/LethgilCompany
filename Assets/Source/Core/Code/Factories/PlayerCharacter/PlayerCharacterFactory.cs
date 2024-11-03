using UnityEngine;

namespace Core
{
    public class PlayerCharacterFactory<T> : IPlayerCharacterFactory where T : IInstantiatable
    {
        private readonly GameObject _characterPrefab;
        private readonly GameObject _playerCameraPrefab;
        private readonly GameObject _mainCameraPrefab;
        private readonly T _instantiatable;

        public PlayerCharacterFactory(T instantiatable, PlayerConfig config)
        {
            _instantiatable = instantiatable;
            _characterPrefab = config.PlayerPrefab;
            _playerCameraPrefab = config.PlayerCameraPrefab;
            _mainCameraPrefab = config.MainCameraPrefab;
        }

        public GameObject CreateCharacter(Vector3 position, Quaternion rotation)
            => _instantiatable.Instantiate(_characterPrefab, position, rotation);

        public GameObject CreatePlayerCamera()
            => Object.Instantiate(_playerCameraPrefab);

        public GameObject CreateMainCamera()
            => Object.Instantiate(_mainCameraPrefab);
    }
}
