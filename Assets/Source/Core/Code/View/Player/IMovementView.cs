namespace Core
{
    public interface IMovementView
    {
        Speed MovementSpeed { get; }
        Jumping Jumping { get; }

        void Construct(Movement model);
        void Render();
    }
}
