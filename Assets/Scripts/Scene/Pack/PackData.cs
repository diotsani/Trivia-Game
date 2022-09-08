using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dio.TriviaGame.Global;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Dio.TriviaGame.Pack
{
    public class PackData : MonoBehaviour
    {
        [SerializeField] private Button _packButtonPrefab;
        [SerializeField] private Transform _packParent;
        private List<int> price = new List<int> {0,100,100,100};
        private int amountPack = 4;

        private void Awake()
        {
            PackType[] packs = (PackType[])Enum.GetValues(typeof(PackType));
            for (int i = 0; i < packs.Length; i++)
            {
                Button packButton = Instantiate(_packButtonPrefab, _packParent);

                PackType packType = packs[i];
                packButton.name = "Level Pack " + packType.ToString();
                packButton.GetComponentInChildren<TMP_Text>().text = "Level Pack " + packType.ToString();
                packButton.GetComponent<PackObject>().pricePack = price[i];

                packButton.onClick.RemoveAllListeners();
                packButton.onClick.AddListener(() => OnClickPack(packType, packButton));
            }
        }
        void OnClickPack(PackType pack,Button button)
        {
            PackDatabase.databaseInstance.packType = pack;
            PackDatabase.databaseInstance.packName = pack.ToString();
            PackDatabase.databaseInstance.GetPackList();

            SceneManager.LoadScene("Level");
        }
    }
    public enum PackType
    {
        A,
        B,
        C,
        D
    }
}