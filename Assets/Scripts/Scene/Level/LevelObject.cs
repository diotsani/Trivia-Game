using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dio.TriviaGame.Global;

namespace Dio.TriviaGame.Level
{
    public class LevelObject : MonoBehaviour
    {
        SaveData saveData = SaveData.saveDataInstance;
        public TMP_Text levelNameLabel;
        public Button selectButton;
        public Image completeImage;
        public string levelNameID;
        public bool isCompleted;

        private void Start()
        {
            if(saveData.playerData.levelIdData.Contains(levelNameID))
            {
                isCompleted = true;
                if(isCompleted)
                {
                    completeImage.gameObject.SetActive(true);
                }
            }
        }
    }
}