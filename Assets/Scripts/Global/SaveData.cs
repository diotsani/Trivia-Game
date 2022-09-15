using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class SaveData : MonoBehaviour
    {
        public static SaveData saveDataInstance;
        public const string _prefsKey = "PlayerData";

        public PlayerData playerData;

        //public int amountCoinData;
        //public List<int> priceData = new List<int>();
        //public List<string> packIdData = new List<string>();
        //public List<string> levelIdData = new List<string>();
        //public List<string> progressLevelData = new List<string>();

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
            //string json = JsonUtility.ToJson(this);
            //PlayerPrefs.SetString(_prefsKey, json);

            string json = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(_prefsKey, json);
            Debug.Log("Save "+json);
        }
        public void Load()
        {
            //if (PlayerPrefs.HasKey(_prefsKey))
            //{
            //    string json = PlayerPrefs.GetString(_prefsKey);
            //    JsonUtility.FromJsonOverwrite(json, this);
            //}
            //else
            //    Save();
            if (PlayerPrefs.HasKey(_prefsKey))
            {
                string json = PlayerPrefs.GetString(_prefsKey);
                playerData = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log("Load " + json);
            }
            else
                Save();
            
        }
        public void AddLevelIdData(string GetName,int GetCoin)
        {
            if (!playerData.levelIdData.Contains(GetName))
            {
                playerData.levelIdData.Add(GetName);
                Currency.currencyInstance.GetCoin(GetCoin);

                Save();
            }
        }
        public void AddPackIdData(string GetName)
        {
            if (!playerData.packIdData.Contains(GetName))
            {
                playerData.packIdData.Add(GetName);
                Save();
            }
        }
        public void AddQuizIdData(string GetName)
        {
            if (!playerData.levelIdData.Contains(GetName))
            {
                playerData.quizIdData.Add(GetName);
                Save();
            }
        }
    }
    [System.Serializable]
    public class PlayerData
    {
        public int amountCoinData;
        public List<int> priceData;
        public List<string> quizIdData;
        public List<string> packIdData;
        public List<string> levelIdData;
        public List<string> progressLevelData;
        public PlayerData()
        {
            amountCoinData = 0;
            priceData = new List<int>() { 0, 100, 100, 100 };
            packIdData = new List<string>();
            levelIdData = new List<string>();
            progressLevelData = new List<string>();
            quizIdData = new List<string>();
        }
    }
}