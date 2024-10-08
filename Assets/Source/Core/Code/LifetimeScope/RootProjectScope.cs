using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class RootProjectScope : LifetimeScope
    {
        [SerializeField] private PlayerConfig _playerConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);

            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);
            builder.Register<PlayerCharacterFactory>(Lifetime.Singleton);
            builder.Register<SceneLoadService>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}
