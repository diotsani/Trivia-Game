using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Dio.TriviaGame.Pack
{
    public class PackObject : MonoBehaviour
    {
        [SerializeField] private Button _lockButton;
        private int free = 0;
        public int pricePack;

        private void Start()
        {
            if (pricePack > free)
            {
                _lockButton.gameObject.SetActive(true);
            }
            else
                _lockButton.gameObject.SetActive(false);
        }
    }
}