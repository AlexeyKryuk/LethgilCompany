using Core.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerSpawnPoint);

            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.Register<PlayerPrefsSaveService<Player>>(Lifetime.Scoped).As<ISaveService<Player>>();
            builder.Register<PlayerSpawnService>(Lifetime.Scoped);
        }
    }
}
