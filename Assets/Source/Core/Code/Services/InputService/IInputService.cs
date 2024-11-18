namespace Core
{
    public interface IInputService<T> where T : struct
    {
        T Inputs { get; }
    }
}
