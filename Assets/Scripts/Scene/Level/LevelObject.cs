using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dio.TriviaGame.Level
{
    public class LevelObject : MonoBehaviour
    {
        public TMP_Text levelNameLabel;
        public Button selectButton;
        public Image completeImage;
        public bool isCompleted;

        private void Update()
        {
            if(isCompleted)
            {
                completeImage.gameObject.SetActive(true);
            }
        }
    }
}