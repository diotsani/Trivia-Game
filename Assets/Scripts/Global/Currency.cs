using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class Currency : MonoBehaviour
    {
        public static Currency currencyInstance;
        public int amountCoin;
        public int getCoin;

        SaveData saveData = SaveData.saveDataInstance;
        private void Awake()
        {
            if (currencyInstance == null)
            {
                currencyInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            saveData.Load();
            amountCoin = saveData.amountCoinData;
        }
        public void GetCoin(int get)
        {
            getCoin = get;
            AddCoin(getCoin);
        }
        public void AddCoin(int add)
        {
            amountCoin += add;
            saveData.amountCoinData = amountCoin;
            getCoin = 0;

            saveData.Save();
        }
        public void SpendCoin(int spend)
        {
            amountCoin -= spend;
        }
    }
}