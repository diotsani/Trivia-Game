using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using UnityEngine;
using UnityEngine.UI;

namespace Dio.TriviaGame.Pack
{
    public class PackObject : MonoBehaviour
    {
        SaveData saveData;
        public PackObject pack;
        public Button lockButton;
        [SerializeField] private Image _completeImage;
        public string packNameID;
        public bool isCompleted;

        private int free = 0;
        public int pricePack;
        private void Start()
        {
            SetLock();
            saveData = SaveData.saveDataInstance;
            if (saveData.packIdData.Contains(packNameID))
            {
                isCompleted = true;
                if (isCompleted)
                {
                    _completeImage.gameObject.SetActive(true);
                }
            }

        }
        void SetLock()
        {
            if (pricePack > free)
            {
                lockButton.gameObject.SetActive(true);
            }
            else
                lockButton.gameObject.SetActive(false);
        }

        public void OnClickLock(Button button, int ID)
        {
            EventManager.TriggerEvent("BuyPackMessage", new BuyPackMessage(pricePack, pack, ID));
        }
        public void RemovePrice(int price)
        {
            pricePack -= price;
        }
        public void RemoveLock()
        {
            lockButton.gameObject.SetActive(false);
        }
    }
}