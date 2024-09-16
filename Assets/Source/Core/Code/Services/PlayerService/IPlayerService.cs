namespace Core
{
    public interface IPlayerService : ILifetimeCycleService
    {
        IPresenter Presenter { get; }
    }
}
