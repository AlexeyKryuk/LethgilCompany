using ItemGrabbing;
using Core.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerSpawnPoint);

            builder.Register<PlayerPrefsSaveService<Player>>(Lifetime.Scoped).As<ISaveService<Player>>();
            builder.Register<GrabbingService>(Lifetime.Scoped).As<IGrabbingService>();
            builder.Register<PlayerService>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}
