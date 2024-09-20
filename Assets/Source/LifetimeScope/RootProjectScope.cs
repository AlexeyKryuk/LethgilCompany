using Core;
using ItemGrabbing;
using Network;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class RootProjectScope : LifetimeScope
    {
        [SerializeField] private StandaloneInputService _inputService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);

            RegisterConfigs(builder);

            builder.Register<SceneLoadService>(Lifetime.Singleton);
            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();

            builder.RegisterInstance<IInputService>(Instantiate(_inputService));

            builder.Register<PlayerCharacterFactory>(Lifetime.Singleton);
            builder.Register<NetworkPlayerCharacterFactory>(Lifetime.Singleton).As<IPlayerCharacterFactory>();
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }

        private static void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(Resources.Load<GrabbingConfig>("Grabbing Config"));
            builder.RegisterInstance(Resources.Load<UIConfig>("UI Config"));
            builder.RegisterInstance(Resources.Load<PlayerConfig>("Player Config"));
        }
    }
}
