using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using UnityEngine;
using UnityEngine.UI;

namespace Dio.TriviaGame.Pack
{
    public class PackObject : MonoBehaviour
    {
        public PackObject pack;
        public Button lockButton;

        private int free = 0;
        public int pricePack;
        private void Start()
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
            EventManager.TriggerEvent("BuyPackMessage", new BuyPackMessage(pricePack,pack,ID));
            SaveData.saveDataInstance.Save();
            //button.gameObject.SetActive(false);
            //button.GetComponent<PackObject>().pricePack = free;
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