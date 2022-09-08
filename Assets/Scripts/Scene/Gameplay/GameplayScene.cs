using Dio.TriviaGame.Global;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dio.TriviaGame.Gameplay
{
    public class GameplayScene : MonoBehaviour
    {
        [SerializeField] Button _xButton;

        private void Awake()
        {
            _xButton.onClick.RemoveAllListeners();
            _xButton.onClick.AddListener(() => QuitGame("Level"));
        }

        private void OnEnable()
        {
            EventManager.StartListening("GoToLevelMessage", GoToLevelScene);
            EventManager.StartListening("GoToPackMessage", GoToPackScene);
        }
        private void OnDisable()
        {
            EventManager.StopListening("GoToLevelMessage", GoToLevelScene);
            EventManager.StopListening("GoToPackMessage", GoToPackScene);
        }

        void QuitGame(string name)
        {
            SceneManager.LoadScene(name);
        }
        void GoToLevelScene()
        {
            SceneManager.LoadScene("Level");
        }
        void GoToPackScene()
        {
            SceneManager.LoadScene("Pack");
        }
    }
}