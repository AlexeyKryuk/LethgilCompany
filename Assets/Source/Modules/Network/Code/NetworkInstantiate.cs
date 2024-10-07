using Core;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class NetworkInstantiate : IInstantiatable
    {
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
            => PhotonNetwork.Instantiate(prefab.name, position, rotation);
    }
}
