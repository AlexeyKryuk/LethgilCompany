using ItemGrabbing;
using VContainer;
using VContainer.Unity;
using Core;
using Core.View;
using Network;
using Customization;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterComponents(builder);
            RegisterServices(builder);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }

        private void RegisterComponents(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerSpawnPoint>();
            builder.RegisterComponentInHierarchy<LootSpawnPoints>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<PlayerService>(Lifetime.Scoped).As<IPlayerService, ILifetimeCycleService>();
            builder.Register<GrabbingService>(Lifetime.Scoped).As<IGrabbingService, ILifetimeCycleService>();
            builder.Register<NetworkLootSpawner>(Lifetime.Scoped).As<ILootSpawner>();
            builder.Register<LootService>(Lifetime.Scoped).As<ILootService, ILifetimeCycleService>();
            builder.Register<CustomizationPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>();
        }
    }
}
