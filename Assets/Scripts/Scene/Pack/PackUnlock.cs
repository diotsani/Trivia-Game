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
        SaveData saveData = SaveData.saveDataInstance;

        [SerializeField] private PackScene _packScene;
        private string _enoughCoin;
        private string _notEnoughCoin;
        private void Start()
        {
            _enoughCoin = "Succes Buy Pack";
            _notEnoughCoin = "Not Enough Coin";
        }
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
                saveData.playerData.priceData[getId] = 0;

                StartCoroutine(_packScene.ShowPopUp(_enoughCoin));

                SaveData.saveDataInstance.Save();
                EventManager.TriggerEvent("TrackUnlockMessage");
            }
            else
            {
                StartCoroutine(_packScene.ShowPopUp(_notEnoughCoin));
            }
        }
    }
}