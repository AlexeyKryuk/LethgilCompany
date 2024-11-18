using CharacterController;
using Combat;
using Core;
using Customization;
using ItemGrabbing;
using Network;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class RootProjectScope : LifetimeScope
    {
        [SerializeField] private PlayerInput _inputServicePrefab;
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

            RegisterInputService(builder);
            RegisterSaveServices(builder);
        }

        private void RegisterInputService(IContainerBuilder builder)
        {
            var inputService = Instantiate(_inputServicePrefab);

            foreach (var service in inputService.GetComponentsInChildren<StandaloneInputService>())
            {
                builder.RegisterInstance(service).AsImplementedInterfaces();
            }
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
            builder.RegisterInstance(Resources.Load<ControllerStaticData>("Controller Data"));
            builder.RegisterInstance(Resources.Load<CombatStaticData>("Combat Data"));
        }

        private void RegisterSaveServices(IContainerBuilder builder)
        {
            builder.Register<PhotonNetworkSaveService<Photon.Realtime.Player>>(Lifetime.Singleton).As<ISaveService<Photon.Realtime.Player>>();
            builder.Register<PhotonNetworkSaveService<CustomizationInfo>>(Lifetime.Singleton).As<ISaveService<CustomizationInfo>>();
            builder.Register<PhotonNetworkSaveService<ControllerData>>(Lifetime.Singleton).As<ISaveService<ControllerData>>();
            builder.Register<PhotonNetworkSaveService<CombatData>>(Lifetime.Singleton).As<ISaveService<CombatData>>();
        }
    }
}
