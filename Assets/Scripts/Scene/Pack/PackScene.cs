using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Dio.TriviaGame.Pack
{
    public class PackScene : MonoBehaviour
    {
        [SerializeField] Button _xButton;
        [SerializeField] Image _popUpImage;
        [SerializeField] TMP_Text _popUpText;

        private void Awake()
        {
            _xButton.onClick.RemoveAllListeners();
            _xButton.onClick.AddListener(() => ChangeScene("Home"));
        }

        void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }
        void SelectPack()
        {

        }
        public IEnumerator ShowPopUp(string GetText)
        {
            _popUpImage.gameObject.SetActive(true);
            _popUpText.text = GetText;
            yield return new WaitForSeconds(2);
            _popUpImage.gameObject.SetActive(false);

        }
    }
}