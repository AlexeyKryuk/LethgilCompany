using Core;
using Core.Model;
using Core.View;
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
        private readonly LootSpawnPoints _spawnPoints;

        private List<GameObject> _pool = new List<GameObject>();

        public NetworkLootSpawner(ILootFactory factory, LootConfig config, LootSpawnPoints spawnPoints)
        {
            _factory = factory;
            _config = config;
            _spawnPoints = spawnPoints;
        }

        public override void Spawn()
        {
            if (PhotonNetwork.IsMasterClient == false)
                return;

            var count = Math.Min(_config.MaxQuantityOnLocation, _spawnPoints.Points.Count);

            foreach (var spawnPoint in ShuffleInternal(_spawnPoints.Points, count))
            {
                var spawned = _factory.Create(LootID.Cube, spawnPoint.transform.position, Quaternion.identity);
                _pool.Add(spawned);
            }
        }
    }
}
