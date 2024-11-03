using Core;
using Customization;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<PlayerSpawnPoint>();
        builder.Register<CustomizationService>(Lifetime.Scoped).As<ICustomizationService, ILifetimeCycleService>();

        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
