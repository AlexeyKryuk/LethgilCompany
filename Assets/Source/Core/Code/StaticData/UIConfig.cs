using System.Collections.Generic;
using UnityEngine;
using System;

namespace Core
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Config/Create UI Config")]
    public class UIConfig : ScriptableObject
    {
        public List<GameObject> Prefabs;

        public GameObject GetPrefab<T>() where T : IUIElement
        {
            foreach (GameObject prefab in Prefabs)
                if (prefab.TryGetComponent(out T _))
                    return prefab;

            throw new NullReferenceException("UIElement " + nameof(T) + " not found");
        }
    }
}
