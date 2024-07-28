namespace Core
{
    public interface ISaveService<T>
    {
        void Save(ISaveLoaded saveLoaded, T model);
        T Load(ISaveLoaded saveLoaded, T byDefault);
    }
}
