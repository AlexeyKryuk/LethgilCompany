using Core;
using System;
using VContainer.Unity;

namespace Customization
{
    public interface ICustomizationService : ILifetimeCycleService, IInitializable, ISaveLoaded, IDisposable
    {

    }
}
