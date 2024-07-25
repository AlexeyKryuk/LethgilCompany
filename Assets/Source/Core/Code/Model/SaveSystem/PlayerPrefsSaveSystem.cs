using UnityEngine;

namespace Core
{
    public class PlayerPrefsSaveSystem<T> : ISaveSystem<T>
    {
        public T Load(ISaveLoaded saveLoaded, T byDefault)
        {
            if (PlayerPrefs.HasKey(saveLoaded.Key))
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveLoaded.Key));

            return byDefault;
        }


        public void Save(ISaveLoaded saveLoaded, T model)
        {
            PlayerPrefs.SetString(saveLoaded.Key, JsonUtility.ToJson(model));
        }
    }
}
