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


        private void Start()
        {
            
        }

        public void SetMaximumWeight(float weight)
        {
            maximumWeight = weight;
        }

        public void UpdatePlayerData(float money, float weight)
        {
            SetPlayerMoney(money);
            SetPlayerTradingWeight(weight);
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
