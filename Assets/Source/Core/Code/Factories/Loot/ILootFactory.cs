using Core.Model;
using UnityEngine;

namespace Core
{
    public interface ILootFactory
    {
        GameObject Create(LootID id, Vector3 position, Quaternion rotation);
    }
}
