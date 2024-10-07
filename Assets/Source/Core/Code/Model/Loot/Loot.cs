namespace Core.Model
{
    public class Loot : ILoot
    {
        private readonly LootID _id;

        public Loot(LootID id)
        {
            _id = id;
        }
    }
}
