using UnityEngine;

namespace RockPaperScissors
{
    [CreateAssetMenu(fileName = "Choice Data", menuName = "Config/Rock-Paper-Scissors/Create Choice Data")]
    public class ChoiceStaticData : ScriptableObject
    {
        [field: SerializeField] public ChoiceType ChoiceType;
        [field: SerializeField] public Color SelectedColor;
        [field: SerializeField] public Sprite Icon;
    }
}
