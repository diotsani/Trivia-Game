using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class SaveData : MonoBehaviour
    {
        public static SaveData saveDataInstance;
        private const string _prefsKey = "SaveData";

        private void Awake()
        {
            if (saveDataInstance == null)
            {
                saveDataInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            Load();
        }
        public void Save()
        {
            string json = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(_prefsKey, json);
        }
        public void Load()
        {
            if (PlayerPrefs.HasKey(_prefsKey))
            {
                string json = PlayerPrefs.GetString(_prefsKey);
                JsonUtility.FromJsonOverwrite(json, this);
            }
            else
                Save();
        }
    }
}