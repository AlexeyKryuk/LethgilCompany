using System.Collections.Generic;
using UnityEngine;

namespace Core.View
{
    public class LootSpawnPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points = new List<Transform>();

        public IReadOnlyList<Transform> Points => _points;

        [ContextMenu("Collect")]
        public void Collect()
        {
            foreach (var point in GetComponentsInChildren<Transform>())
                if (point != transform)
                    _points.Add(point);
        }
    }
}
