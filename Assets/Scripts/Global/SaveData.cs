using Dio.TriviaGame.Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using static UnityEditor.Progress;

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
            //Debug.Log("Save "+json);
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
                //Debug.Log("Load " + json);
            }
            else
                Save();
            
        }
        public void AddProgressLevelData(string GetName)
        {
            if (!playerData.progressLevelData.Contains(GetName))
            {
                playerData.progressLevelData.Add(GetName);
                Save();
            }
        }
        public void CheckProgressLevel(List<QuizData> GetQuiz, string GetName)
        {
            //List<int> ints = new List<int> { 0, 1, 2, 3, 4 };
            
            if (playerData.progressLevelData.Count == GetQuiz.Count)
            {
                AddPackIdData(GetName);
                playerData.progressLevelData.Clear();
                Save();
            }
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
        public void AddQuizIdData()
        {
            List<string> strings = new List<string> { "A0,A1,A2,A3,A4" };
            foreach (var item in strings)
            {
                var n = playerData.levelIdData.FirstOrDefault(x => x == item);
                Debug.Log(n);
            }
            
            //if (!playerData.levelIdData.Contains(GetName))
            //{
            //    playerData.quizIdData.Add(GetName);
            //    Save();
            //}

            
        }
        public bool tes()
        {
            List<string> strings = new List<string> { "A0,A1,A2,A3,A4" };
            foreach (var item in strings)
            {
                
                if (!playerData.progressLevelData.Contains(item))
                {
                    return false;
                }
            }
            return true;
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