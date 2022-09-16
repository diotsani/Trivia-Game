using Dio.TriviaGame.Database;
using Dio.TriviaGame.Level;
using Dio.TriviaGame.Pack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Dio.TriviaGame.Global
{
    public class PackDatabase : MonoBehaviour
    {
        public static PackDatabase databaseInstance;

        public QuizScriptable levelPackSelected;
        public int indexLevel;
        public string packName;

        public PackType packType;
        SaveData _saveData;
        private void Awake()
        {
            if (databaseInstance == null)
            {
                databaseInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            _saveData = SaveData.saveDataInstance;
        }

        public void GetNamePack(string name)
        {
            packName = name;
        }

        public void GetPackList()
        {
            levelPackSelected = Resources.Load<QuizScriptable>("Scriptable/Pack "+packName);
        }

        public void GetLevelList()
        {

        }
        public void GetLevelData()
        {

        }
    }
}