using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Core
{
    public abstract class LootSpawner : ILootSpawner
    {
        public abstract void Spawn();

        protected IEnumerable<T> ShuffleInternal<T>(IReadOnlyList<T> values, int count)
        {
            var array = new List<T>(values);

            for (var n = 0; n < count; n++)
            {
                var k = Random.Range(n, array.Count);
                var temp = array[n];

                array[n] = array[k];
                array[k] = temp;
            }

            return values;
        }
    }
}
