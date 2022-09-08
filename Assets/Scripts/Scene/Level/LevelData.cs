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
        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private Transform _levelParent;
        [SerializeField] private Image imageCompleted;
        private List<LevelObject> levelList;
        private string levelName;
        private int amountLevel = 5;
        private bool _isCompleted;

        private void Awake()
        {
            levelList = new List<LevelObject>();
            GetLevelList();
            InitLevelList();
        }
        private void Update()
        {
            //AllLevelCompleted();
        }
        public void GetLevelList()
        {
            levelName = PackDatabase.databaseInstance.packName;
        }
        public void InitLevelList()
        {
            for (int i = 0; i < amountLevel; i++)
            {
                int indexLv = i;
                int lvName = i + 1;
                Button levelButton = Instantiate(_levelButtonPrefab, _levelParent);
                levelList.Add(levelButton.GetComponent<LevelObject>());
                levelButton.name = "Level " + levelName + "-" + lvName;
                levelButton.GetComponent<LevelObject>().levelNameLabel.text = "Level " + levelName + "-" + lvName;
                levelButton.GetComponent<LevelObject>().isCompleted = PackDatabase.databaseInstance.levelPackSelected.quizData[i].isComplete;

                levelButton.onClick.RemoveAllListeners();
                levelButton.onClick.AddListener(() => OnClickPack(levelButton, indexLv));
            }
        }
        void AllLevelCompleted()
        {
            for (int i = 0; i < levelList.Count; i++)
            {
                if(levelList[i].isCompleted)
                {
                    imageCompleted.gameObject.SetActive(true);
                }
            }
        }
        public void LoadLevelList()
        {

        }
        void OnClickPack(Button button, int index)
        {
            PackDatabase.databaseInstance.levelIndex = index;
            EventManager.TriggerEvent("SelectLevelMessage");
        }
    }
}