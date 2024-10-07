namespace Core
{
    public class LootService : ILootService
    {
        private readonly ILootSpawner _spawner;

        public LootService(ILootSpawner spawner)
        {
            _spawner = spawner;
        }

        public void Initialize()
        {
            _spawner.Spawn();
        }
    }
}
