using Core;
using Core.Model;
using Customization;
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
        [SerializeField] private string _gameScene;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);

            RegisterConfigs(builder);
            RegisterServices(builder);
            RegisterFactories(builder);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<SceneLoadService>(Lifetime.Singleton).WithParameter(_gameScene);
            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();
            builder.RegisterInstance<IInputService>(Instantiate(_inputService));

            RegisterSaveServices(builder);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<NetworkInstantiate>(Lifetime.Singleton);
            builder.Register<ObjectResolveInstantiate>(Lifetime.Singleton);

            builder.Register<PlayerCharacterFactory<NetworkInstantiate>>(Lifetime.Singleton).As<IPlayerCharacterFactory>();
            builder.Register<LootFactory<NetworkInstantiate>>(Lifetime.Singleton).As<ILootFactory>();
            builder.Register<UIFactory>(Lifetime.Singleton).As<IUIFactory>();
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(Resources.Load<GrabbingConfig>("Grabbing Config"));
            builder.RegisterInstance(Resources.Load<UIConfig>("UI Config"));
            builder.RegisterInstance(Resources.Load<PlayerConfig>("Player Config"));
            builder.RegisterInstance(Resources.Load<LootConfig>("Loot Config"));
        }

        private void RegisterSaveServices(IContainerBuilder builder)
        {
            builder.Register<PhotonNetworkSaveService<Player>>(Lifetime.Singleton).As<ISaveService<Player>>();
            builder.Register<PhotonNetworkSaveService<CustomizationInfo>>(Lifetime.Singleton).As<ISaveService<CustomizationInfo>>();
        }
    }
}
