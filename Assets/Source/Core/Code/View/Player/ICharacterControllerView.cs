using UnityEngine;

namespace Core.View
{
    public interface ICharacterControllerView
    {
        Transform Transform { get; }

        void SpecifyCameraTransform(Transform transform);
        void UpdateInputs(ICharacterInputs inputs);
    }
}
