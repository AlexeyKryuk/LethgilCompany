using Core;
using System;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerPresenter : IPlayerPresenter, ILifetimeCycleService, IInitializable
    {
        private readonly IPlayerCharacterFactory _factory;

        private GameObject _playerInstance;
        private GameObject _playerCameraInstance;

        public PlayerPresenter(IPlayerCharacterFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _factory.CreateMainCamera();

            var spawnPoints = GameObject.FindGameObjectsWithTag(GameObjectTags.PlayerSpawnPoint.ToString());
            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];

            _playerInstance = _factory.CreateCharacter(point.transform.position, point.transform.rotation);
            //_playerCameraInstance = _factory.CreatePlayerCamera(point.transform.position, point.transform.rotation);
        }

        public T GetView<T>()
        {
            T characterview = _playerInstance.GetComponentInChildren<T>();

            if (characterview == null)
                characterview = _playerInstance.GetComponentInParent<T>();

            //if (characterview == null)
            //    characterview = _playerCameraInstance.GetComponentInChildren<T>();

            if (characterview == null)
                throw new NullReferenceException($"Component {typeof(T)} not found!");

            return characterview;
        }
    }
}
