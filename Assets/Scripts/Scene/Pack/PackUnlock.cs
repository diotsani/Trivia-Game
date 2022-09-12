using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Pack
{
    public class PackUnlock : MonoBehaviour
    {
        public PackData packData;
        int getCoin;
        int getId;
        PackObject getPack;
        private void OnEnable()
        {
            EventManager.StartListening("BuyPackMessage", UnlockPack);
        }
        private void OnDisable()
        {
            EventManager.StopListening("BuyPackMessage", UnlockPack);
        }
        void UnlockPack(object coinData)
        {
            BuyPackMessage message = (BuyPackMessage)coinData;
            getCoin = message.spend;
            getPack = message.pack;
            getId = message.idPack;
            if(Currency.currencyInstance.amountCoin >= getCoin)
            {
                Currency.currencyInstance.SpendCoin(getCoin);
                getPack.RemovePrice(getCoin);
                getPack.RemoveLock();
                packData.price[getId] = 0;

                SaveData.saveDataInstance.Save();
                EventManager.TriggerEvent("TrackUnlockMessage");
                EventManager.TriggerEvent("SetCoinText");
            }
            else
            {
                // Display not enough coin
            }
        }
    }
}