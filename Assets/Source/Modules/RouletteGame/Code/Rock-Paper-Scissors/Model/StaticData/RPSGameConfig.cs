using UnityEngine;

namespace RockPaperScissors
{
    [CreateAssetMenu(fileName = "RPS Game Config", menuName = "Config/Rock-Paper-Scissors/Create Game Config", order = 1)]
    public class RPSGameConfig : ScriptableObject
    {
        [field: SerializeField] public int NumberOfPlayers;
        [field: SerializeField] public ChoiceStaticData[] Choices;
    }
}
