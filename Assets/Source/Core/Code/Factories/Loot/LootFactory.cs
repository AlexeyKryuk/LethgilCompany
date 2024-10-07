using Core.Model;
using UnityEngine;

namespace Core
{
    public class LootFactory<T> : ILootFactory where T : IInstantiatable
    {
        private readonly LootConfig _config;
        private readonly T _instantiatable;

        public LootFactory(T instantiatable, LootConfig config)
        {
            _config = config;
            _instantiatable = instantiatable;
        }

        public GameObject Create(LootID id, Vector3 position, Quaternion rotation)
            => _instantiatable.Instantiate(_config.GetPrefab(id), position, rotation);
    }
}
