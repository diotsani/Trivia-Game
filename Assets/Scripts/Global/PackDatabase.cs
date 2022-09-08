using Dio.TriviaGame.Database;
using Dio.TriviaGame.Pack;
using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class PackDatabase : MonoBehaviour
    {
        public static PackDatabase databaseInstance;
        public QuizScriptable levelPackSelected;
        public int levelIndex;
        public string packName;

        public PackType packType;

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