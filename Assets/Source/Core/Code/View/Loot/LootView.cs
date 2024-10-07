using Core.Model;
using UnityEngine;

namespace Core.View
{
    public class LootView : MonoBehaviour, ILootView
    {
        [field: SerializeField] public LootID ID { get; private set; }
    }
}
