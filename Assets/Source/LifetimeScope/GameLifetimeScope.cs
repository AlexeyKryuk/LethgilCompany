using ItemGrabbing;
using VContainer;
using VContainer.Unity;
using Core;
using Network;
using Player;
using Combat;
using CharacterController;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterPresenters(builder);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            //builder.Register<NetworkLootSpawner>(Lifetime.Scoped).As<ILootSpawner>();
            //builder.Register<LootService>(Lifetime.Scoped).As<ILootService, ILifetimeCycleService>();
            builder.Register<CustomizationPhotonPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>();
        }

        private void RegisterPresenters(IContainerBuilder builder)
        {
            builder.Register<PlayerPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().As<IPlayerPresenter>();
            builder.Register<CharacterControllerPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().AsSelf();
            //builder.Register<CharacterCombatPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().AsSelf();
            //builder.Register<GrabbingPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().AsSelf();
        }
    }
}
