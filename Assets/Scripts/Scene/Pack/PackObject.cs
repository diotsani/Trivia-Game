using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
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
        private void OnEnable()
        {
            EventManager.StartListening("CanBuy", RemovePrice);
        }
        private void OnDisable()
        {
            EventManager.StopListening("CanBuy", RemovePrice);
        }
        private void Start()
        {
            _lockButton.onClick.RemoveAllListeners();
            _lockButton.onClick.AddListener(OnClickLock);
        }
        private void Update()
        {
            if (pricePack > free)
            {
                _lockButton.gameObject.SetActive(true);
            }
            else
            {
                _lockButton.gameObject.SetActive(false);
            }
        }

        void OnClickLock()
        {
            EventManager.TriggerEvent("BuyPackMessage",new BuyPackMessage(pricePack));
            pricePack = free;
        }
        void RemovePrice()
        {
            //pricePack = free;
        }
    }
}