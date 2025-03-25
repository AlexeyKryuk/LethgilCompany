using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
    public class Rules
    {
        private readonly int _numberOfPlayers;

        public Rules(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
        }

        public void Play(List<Participant> participants)
        {
            if (participants.Count != _numberOfPlayers)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                for (int j = 0; j < _numberOfPlayers; j++)
                {
                    if (i == j)
                        continue;

                    participants[i].PlayWith(participants[j]);
                }
            }
        }
    }
}
