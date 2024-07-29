using Core.Model;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Config/Create Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public GameObject Prefab;
        public Camera CameraPrefab;

        public TransformSettings TransformSettings;
        public DamageSettings DamageSettings;
    }
}
