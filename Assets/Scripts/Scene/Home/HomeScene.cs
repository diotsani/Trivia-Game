using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dio.TriviaGame.Home
{
    public class HomeScene : MonoBehaviour
    {
        [SerializeField] Button _playButton;

        private void Awake()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(() => StartPlay("Pack"));
        }

        void StartPlay(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}

