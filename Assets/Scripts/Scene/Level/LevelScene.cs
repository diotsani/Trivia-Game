using Dio.TriviaGame.Global;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dio.TriviaGame.Level
{
    public class LevelScene : MonoBehaviour
    {
        [SerializeField] Button _backButton;
        private void OnEnable()
        {
            EventManager.StartListening("SelectLevelMessage", SelectLevel);
        }
        private void OnDisable()
        {
            EventManager.StopListening("SelectLevelMessage", SelectLevel);
        }
        private void Awake()
        {
            _backButton.onClick.RemoveAllListeners();
            _backButton.onClick.AddListener(() => ChangeScene("Pack"));
        }

        void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void SelectLevel()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}