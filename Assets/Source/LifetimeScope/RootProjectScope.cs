using Core;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using VContainer;
using VContainer.Unity;

namespace LifetimeScopes
{
    public class RootProjectScope : LifetimeScope
    {
        [SerializeField] private StandaloneInputService _inputService;

        [Header("Configs")]
        [SerializeField] private UIConfig _uiConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Singleton);

            builder.RegisterInstance(_uiConfig);
            builder.RegisterInstance(_playerConfig);

            builder.Register<SceneLoadService>(Lifetime.Singleton);
            builder.Register<UIService>(Lifetime.Singleton).As<IUIService>();

            builder.RegisterComponentInNewPrefab(typeof(IInputService), _inputService, Lifetime.Singleton).DontDestroyOnLoad();

            builder.Register<PlayerCharacterFactory>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}
