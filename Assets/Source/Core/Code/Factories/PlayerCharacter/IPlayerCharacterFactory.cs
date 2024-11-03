using UnityEngine;

namespace Core
{
    public interface IPlayerCharacterFactory
    {
        GameObject CreateCharacter(Vector3 position, Quaternion rotation);
        GameObject CreateMainCamera();
        GameObject CreatePlayerCamera();
    }
}
