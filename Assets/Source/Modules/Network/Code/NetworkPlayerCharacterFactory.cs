using Core;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class NetworkPlayerCharacterFactory : IPlayerCharacterFactory
    {
        private readonly GameObject _prefab;
        private readonly IPlayerCharacterFactory _characterFactory;

        public NetworkPlayerCharacterFactory(PlayerCharacterFactory factory, PlayerConfig config)
        {
            _characterFactory = factory;
            _prefab = config.PlayerPrefab;
        }

        public GameObject Create(Vector3 position, Quaternion rotation)
            => PhotonNetwork.Instantiate(_prefab.name, position, rotation);

        public GameObject CreatePlayerCamera()
            => _characterFactory.CreatePlayerCamera();

        public GameObject CreateMainCamera()
            => _characterFactory.CreateMainCamera();
    }
}
