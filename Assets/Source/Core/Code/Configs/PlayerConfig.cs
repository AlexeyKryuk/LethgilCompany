using Core.Model;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Config/Create Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public GameObject Prefab;
        public GameObject PlayerCameraPrefab;
        public GameObject MainCameraPrefab;

        public TransformSettings TransformSettings;
        public DamageSettings DamageSettings;
    }
}
