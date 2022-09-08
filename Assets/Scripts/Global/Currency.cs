using System.Collections;
using UnityEngine;

namespace Dio.TriviaGame.Global
{
    public class Currency : MonoBehaviour
    {
        public static Currency currencyInstance;
        public int amountCoin = 100;
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
        public void GetCoin()
        {

        }
        public void AddCoin()
        {
            amountCoin += 20;
        }
        public void SpendCoin(int spend)
        {
            amountCoin -= spend;
        }
    }
}