using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Config/Player/Create Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject PlayerCameraPrefab { get; private set; }
        [field: SerializeField] public GameObject MainCameraPrefab { get; private set; }
    }
}
