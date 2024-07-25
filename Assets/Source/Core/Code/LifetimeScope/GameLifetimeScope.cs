using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(FindObjectOfType<PlayerSpawnPoint>());

            builder.RegisterEntryPoint<GameEntryPoint>();
            builder.Register<PlayerSpawnService>(Lifetime.Scoped);
        }
    }
}
