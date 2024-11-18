using UnityEngine;

namespace CharacterController
{
    [CreateAssetMenu(fileName = "Controller Data", menuName = "Config/Player/Create Controller Data")]
    public class ControllerStaticData : ScriptableObject
    {
        [field : SerializeField] public ControllerSettings ControllerSettings { get; private set; }
    }
}
