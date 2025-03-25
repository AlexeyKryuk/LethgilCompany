using VContainer;
using VContainer.Unity;
using Core;
using Network;
using Player;
using CharacterController;
using RockPaperScissors;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterPresenters(builder);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }

        private void RegisterPresenters(IContainerBuilder builder)
        {
            builder.Register<CustomizationPhotonPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>();
            builder.Register<Player.PlayerPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().As<IPlayerPresenter>();
            builder.Register<RockPaperScissors.PlayerPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>();
            builder.Register<CharacterControllerPresenter>(Lifetime.Scoped).As<ILifetimeCycleService>().AsSelf();
        }
    }
}
