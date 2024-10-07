using Core.Model;
using Core.View;
using System.Linq;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Loot Config", menuName = "Config/Loot/Create Loot Config")]
    public class LootConfig : ScriptableObject
    {
        [SerializeField] private LootView[] _prefabs;
        [SerializeField] private int _maxQuantityOnLocation;

        public int MaxQuantityOnLocation => _maxQuantityOnLocation;

        public GameObject GetPrefab(LootID id)
            => _prefabs.FirstOrDefault((loot) => loot.ID == id).gameObject;
    }
}
