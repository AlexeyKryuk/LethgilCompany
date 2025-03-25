namespace RockPaperScissors
{
    public class Participant
    {
        private readonly string _name;

        public Participant(string name)
        {
            _name = name;
        }

        public Choice Choice { get; private set; }
        public int Points { get; private set; }
        public bool IsReady { get; private set; } = false;

        public string Name => _name;

        public void Pick(Choice choice)
        {
            Choice = choice;
            IsReady = true;
        }

        public void PlayWith(Participant player)
        {
            Points = Choice.PlayWith(player.Choice);
            IsReady = false;
        }
    }
}