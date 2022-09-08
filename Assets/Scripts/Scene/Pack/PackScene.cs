using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dio.TriviaGame.Pack
{
    public class PackScene : MonoBehaviour
    {
        [SerializeField] Button _xButton;

        private void Awake()
        {
            _xButton.onClick.RemoveAllListeners();
            _xButton.onClick.AddListener(() => ChangeScene("Home"));
        }

        void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}