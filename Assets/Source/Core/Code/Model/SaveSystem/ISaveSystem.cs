namespace Core
{
    public interface ISaveSystem<T>
    {
        void Save(ISaveLoaded saveLoaded, T model);
        T Load(ISaveLoaded saveLoaded, T byDefault);
    }
}
