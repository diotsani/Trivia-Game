using Dio.TriviaGame.Global;
using Dio.TriviaGame.Message;
using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Pack
{
    public class PackUnlock : MonoBehaviour
    {
        int getCoin;
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
            if(Currency.currencyInstance.amountCoin >= getCoin)
            {
                Currency.currencyInstance.SpendCoin(getCoin);
                EventManager.TriggerEvent("TrackUnlockMessage");
                EventManager.TriggerEvent("CanBuy");
                EventManager.TriggerEvent("SetCoinText");
            }
            else
            {
                
            }
        }
    }
}