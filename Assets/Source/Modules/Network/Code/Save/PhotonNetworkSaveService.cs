using Core;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class PhotonNetworkSaveService<T> : ISaveService<T>
    {
        public T Load(ISaveLoaded saveLoaded, T byDefault)
        {
            string key = saveLoaded.Key + PhotonNetwork.LocalPlayer.NickName;

            if (PlayerPrefs.HasKey(key))
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));

            return byDefault;
        }

        public void Save(ISaveLoaded saveLoaded, T model)
        {
            string key = saveLoaded.Key + PhotonNetwork.LocalPlayer.NickName;

            PlayerPrefs.SetString(key, JsonUtility.ToJson(model));
        }
    }
}
