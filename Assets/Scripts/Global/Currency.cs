using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class Currency : MonoBehaviour
    {
        public static Currency currencyInstance;
        public int amountCoin = 100;
        public int getCoin;
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
        public void GetCoin(int get)
        {
            getCoin = get;
            AddCoin(getCoin);
        }
        public void AddCoin(int add)
        {
            amountCoin += add;
            getCoin = 0;
        }
        public void SpendCoin(int spend)
        {
            amountCoin -= spend;
        }
    }
}