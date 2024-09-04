namespace Core
{
    public interface IPresenter
    {
        T GetView<T>();
    }
}
