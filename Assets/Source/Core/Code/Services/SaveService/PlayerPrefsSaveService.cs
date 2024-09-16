using UnityEngine;

namespace Core
{
    public class PlayerPrefsSaveService<T> : ISaveService<T>
    {
        public T Load(ISaveLoaded saveLoaded, T byDefault)
        {
            Debug.Log(PlayerPrefs.GetString(saveLoaded.Key, $"Для {saveLoaded.Key} сохранений нет"));

            if (PlayerPrefs.HasKey(saveLoaded.Key))
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveLoaded.Key));

            return byDefault;
        }

        public void Save(ISaveLoaded saveLoaded, T model)
        {
            Debug.Log(JsonUtility.ToJson(model));

            PlayerPrefs.SetString(saveLoaded.Key, JsonUtility.ToJson(model));
            PlayerPrefs.Save();
        }
    }
}
