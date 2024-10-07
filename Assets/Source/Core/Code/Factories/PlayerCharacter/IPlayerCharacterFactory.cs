using UnityEngine;

namespace Core
{
    public interface IPlayerCharacterFactory
    {
        GameObject Create(Vector3 position, Quaternion rotation);
        GameObject CreateMainCamera();
        GameObject CreatePlayerCamera();
    }
}
