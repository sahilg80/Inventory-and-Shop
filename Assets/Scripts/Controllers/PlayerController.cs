using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services;
using Assets.Scripts.Utilities;
using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PlayerController
    {
        private const float startingOccupiedWeight = 0f;
        private PlayerModel playerModel;
        private PlayerView playerView;

        public PlayerController(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            playerModel = new PlayerModel(playerSO);
            this.playerView = playerView;
            playerView.SetMaximumWeight(playerModel.MaximumWeight);
            playerView.UpdatePlayerData(playerModel.CurrentOccupiedMoney, startingOccupiedWeight);
            EventService.Instance.OnBuySelectedItem.AddListener(OnBuySelectedItem);
            EventService.Instance.OnSoldSelectedItem.AddListener(OnSoldSelectedItem);
        }

        ~PlayerController()
        {
            EventService.Instance.OnBuySelectedItem.RemoveListener(OnBuySelectedItem);
            EventService.Instance.OnSoldSelectedItem.RemoveListener(OnSoldSelectedItem);
        }

        private void OnBuySelectedItem(TradeDetail detail)
        {
            float amount = playerModel.CurrentOccupiedMoney - detail.TradedAmount;
            playerModel.UpdateOccupiedMoney(amount);
            float weight = playerModel.CurrentOccupiedWeight + detail.TradedWeight;
            playerModel.UpdateOccupiedWeight(weight);
            playerView.UpdatePlayerData(amount, weight);
        }

        private void OnSoldSelectedItem(TradeDetail detail)
        {
            float amount = playerModel.CurrentOccupiedMoney + detail.TradedAmount;
            playerModel.UpdateOccupiedMoney(amount);
            float weight = playerModel.CurrentOccupiedWeight - detail.TradedWeight;
            playerModel.UpdateOccupiedWeight(weight);
            playerView.UpdatePlayerData(amount, weight);
        }

        public bool ValidateAmount(float quantity, float eachItemPrice)
        {
            float amount = quantity * eachItemPrice;
            if(amount <= playerModel.CurrentOccupiedMoney)
            {
                return true;
            }
            return false;
        }

        public bool ValidateWeight(float quantity, float eachItemWeight)
        {
            float weight = quantity * eachItemWeight;
            float value = playerModel.MaximumWeight - playerModel.CurrentOccupiedWeight;
            if (weight <= value)
            {
                return true;
            }
            return false;
        }
    }
}
