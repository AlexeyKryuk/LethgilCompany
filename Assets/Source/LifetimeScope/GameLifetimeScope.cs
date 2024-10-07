using ItemGrabbing;
using Core.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core;
using Core.View;
using Network;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerSpawnPoint);
            builder.RegisterComponentInHierarchy<LootSpawnPoints>();

            RegisterServices(builder);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<PlayerPrefsSaveService<Player>>(Lifetime.Scoped).As<ISaveService<Player>>();
            builder.Register<PlayerService>(Lifetime.Scoped).As<IPlayerService, ILifetimeCycleService>();
            builder.Register<GrabbingService>(Lifetime.Scoped).As<IGrabbingService, ILifetimeCycleService>();

            builder.Register<NetworkLootSpawner>(Lifetime.Scoped).As<ILootSpawner>();
            builder.Register<LootService>(Lifetime.Scoped).As<ILootService, ILifetimeCycleService>();
        }
    }
}
