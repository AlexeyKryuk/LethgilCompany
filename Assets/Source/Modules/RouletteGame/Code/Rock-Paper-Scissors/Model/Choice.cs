using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    [Serializable]
    public struct Choice
    {
        private static Dictionary<ChoiceType, ChoiceType> Table = new Dictionary<ChoiceType, ChoiceType>()
        {
            { ChoiceType.Paper, ChoiceType.Rock },
            { ChoiceType.Rock, ChoiceType.Scissors },
            { ChoiceType.Scissors, ChoiceType.Paper },
        };

        public ChoiceType[] Hands;

        public Choice(params ChoiceType[] choices)
        {
            Hands = choices;
        }

        public int PlayWith(Choice choice)
        {
            int result = 0;

            foreach (var hand in Hands)
            {
                foreach (var oppenentHand in choice.Hands)
                {
                    if (Table[hand] == oppenentHand)
                        result++;
                }
            }

            return result;
        }
    }
}
