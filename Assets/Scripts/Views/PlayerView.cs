using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI playerMoneyText;
        [SerializeField]
        private TextMeshProUGUI playerInventoryWeight;
        private float maximumWeight;
        private const float initialWeight = 0f;


        private void Start()
        {
            
        }

        public void SetInitialPlayerData(float money, float weight)
        {
            maximumWeight = weight;
            SetPlayerMoney(money);
            SetPlayerTradingWeight(initialWeight);
        }

        private void SetPlayerMoney(float money)
        {
            playerMoneyText.SetText(money.ToString());
        }

        private void SetPlayerTradingWeight(float weight)
        {
            playerInventoryWeight.SetText(weight.ToString() + "/" + maximumWeight.ToString());
        }
    }
}
