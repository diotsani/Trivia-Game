using Dio.TriviaGame.Global;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dio.TriviaGame.Level
{
    public class LevelData : MonoBehaviour
    {
        SaveData _saveData = SaveData.saveDataInstance;
        PackDatabase _packDatabase = PackDatabase.databaseInstance;

        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private Transform _levelParent;
        [SerializeField] private int amountLevel;
        private List<LevelObject> levelList;
        private string levelNameData;
        private void Awake()
        {
            levelList = new List<LevelObject>();

            amountLevel = _packDatabase.levelPackSelected.quizData.Count;

            GetLevelList();
            InitLevelList();
        }

        public void GetLevelList()
        {
            levelNameData = _packDatabase.packName;
        }
        public void InitLevelList()
        {
            for (int i = 0; i < amountLevel; i++)
            {
                int indexLv = i;
                int lvName = i + 1;
                Button levelButton = Instantiate(_levelButtonPrefab, _levelParent);
                levelList.Add(levelButton.GetComponent<LevelObject>());
                levelButton.name = "Level " + levelNameData + "-" + lvName;
                levelButton.GetComponent<LevelObject>().levelNameLabel.text = "Level " + levelNameData + "-" + lvName;
                levelButton.GetComponent<LevelObject>().levelNameID = levelNameData + indexLv;

                levelButton.onClick.RemoveAllListeners();
                levelButton.onClick.AddListener(() => OnClickPack(levelButton, indexLv));
            }
            
        }
        void OnClickPack(Button button, int index)
        {
            PackDatabase.databaseInstance.indexLevel = index;
            EventManager.TriggerEvent("SelectLevelMessage");
        }
    }
}