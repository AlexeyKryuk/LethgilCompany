using Core;
using Core.Model;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class NetworkLootSpawner : LootSpawner
    {
        private readonly ILootFactory _factory;
        private readonly LootConfig _config;

        private List<GameObject> _pool = new List<GameObject>();

        public NetworkLootSpawner(ILootFactory factory, LootConfig config)
        {
            _factory = factory;
            _config = config;
        }

        public override void Spawn()
        {
            if (PhotonNetwork.IsMasterClient == false)
                return;

            var spawnPoints = GameObject.FindGameObjectsWithTag(GameObjectTags.LootSpawnPoint.ToString());
            var count = Math.Min(_config.MaxQuantityOnLocation, spawnPoints.Length);

            foreach (var spawnPoint in ShuffleInternal(spawnPoints, count))
            {
                var spawned = _factory.Create(LootID.Cube, spawnPoint.transform.position, Quaternion.identity);
                _pool.Add(spawned);
            }
        }
    }
}
