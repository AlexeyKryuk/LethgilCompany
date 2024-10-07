using UnityEngine;

namespace Core
{
    public interface IInstantiatable
    {
        GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}