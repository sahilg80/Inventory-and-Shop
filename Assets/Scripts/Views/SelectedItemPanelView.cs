using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Utilities;
using UnityEngine.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Views
{
    public class SelectedItemPanelView : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectedItemPanel;
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private TextMeshProUGUI itemNameText;
        [SerializeField]
        private TextMeshProUGUI itemTypeText;
        [SerializeField]
        private TextMeshProUGUI itemRarityTypeText;
        [SerializeField]
        private TextMeshProUGUI itemWeightText;
        [SerializeField]
        private TextMeshProUGUI itemDescriptionText;
        [SerializeField]
        private TextMeshProUGUI itemPriceText;
        [SerializeField]
        private TextMeshProUGUI itemQuantityText;
        [SerializeField]
        private GameObject buyItemButton;
        [SerializeField]
        private GameObject sellItemButton;

        private int currentSelectedQuantity;
        private int remainingQuanity;
        private ItemDetail selectedItemDetails;
        private const string NotValidAmountText = "Amount insufficient";
        private const string NotValidWeightText = "Weight insufficient";

        private void OnEnable()
        {
            EventService.Instance.OnClickItemFromList.AddListener(OnSelectItem);
        }

        private void Start()
        {
            ShowSelectedItemDetailPanel(false);
        }

        public void OnClickIncreaseQuantity()
        {
            if (remainingQuanity <= currentSelectedQuantity) return;

            currentSelectedQuantity = currentSelectedQuantity + 1;
            SetQuantity(currentSelectedQuantity);
        }

        public void OnClickDecreaseQuantity()
        {
            if (currentSelectedQuantity == 0) return;

            currentSelectedQuantity = currentSelectedQuantity - 1;
            SetQuantity(currentSelectedQuantity);
        }

        public void OnClickBuyButton()
        {
            bool isValid = IsThisTradeValid(selectedItemDetails.ItemData.Weight, selectedItemDetails.ItemData.BuyPrice);
            if (!isValid) return;

            EventService.Instance.OnTradeSelectedItem.InvokeEvent(BuyValidItem);
        }

        public void OnClickSellButton()
        {
            EventService.Instance.OnTradeSelectedItem.InvokeEvent(SellValidItem);
        }

        public void SetQuantity(int quantity)
        {
            currentSelectedQuantity = quantity;
            itemQuantityText.SetText(quantity.ToString());
        }

        private void BuyValidItem()
        {
            TradeDetail itemBoughtDetail = new TradeDetail()
            {
                ItemData = selectedItemDetails.ItemData,
                QuantityTraded = currentSelectedQuantity,
                TradedAmount = currentSelectedQuantity * selectedItemDetails.ItemData.BuyPrice,
                TradedWeight = currentSelectedQuantity * selectedItemDetails.ItemData.Weight,
                RemainingQuantity = selectedItemDetails.QuantityAvaiableInCurrentTradeType - currentSelectedQuantity,
            };

            EventService.Instance.OnBuySelectedItem.InvokeEvent(itemBoughtDetail);

            ShowSelectedItemDetailPanel(false);
            ResetProperties();
        }

        private void SellValidItem()
        {
            TradeDetail itemSoldDetail = new TradeDetail()
            {
                ItemData = selectedItemDetails.ItemData,
                QuantityTraded = currentSelectedQuantity,
                TradedAmount = currentSelectedQuantity * selectedItemDetails.ItemData.SellPrice,
                TradedWeight = currentSelectedQuantity * selectedItemDetails.ItemData.Weight,
                RemainingQuantity = selectedItemDetails.QuantityAvaiableInCurrentTradeType - currentSelectedQuantity,
            };

            EventService.Instance.OnSoldSelectedItem.InvokeEvent(itemSoldDetail);

            ShowSelectedItemDetailPanel(false);
            ResetProperties();
        }


        private bool IsThisTradeValid(float eachItemWeight, float eachItemPrice)
        {
            if (currentSelectedQuantity == 0) return false;

            bool value = GameService.Instance.GetPlayerController().ValidateAmount(currentSelectedQuantity, eachItemPrice);

            if (!value)
            {
                EventService.Instance.OnShowTradeStatus.InvokeEvent(NotValidAmountText);
                return false;
            }
            value = GameService.Instance.GetPlayerController().ValidateWeight(currentSelectedQuantity, eachItemWeight);

            if (!value)
            {
                EventService.Instance.OnShowTradeStatus.InvokeEvent(NotValidWeightText);
                return false;
            }
            return true;
        }

        private void OnSelectItem(ItemDetail item, TradeType type)
        {
            selectedItemDetails = item;
            SetParameters(item.ItemData, item.QuantityAvaiableInCurrentTradeType, type);
            ShowSelectedItemDetailPanel(true);
        }

        private void SetParameters(ItemScriptableObject itemSO, int quantity, TradeType type)
        {
            itemIcon.sprite = itemSO.Icon;
            itemNameText.SetText(itemSO.Name);
            itemTypeText.SetText(itemSO.Type.ToString());
            itemDescriptionText.SetText(itemSO.Description);
            itemRarityTypeText.SetText(itemSO.Rarity.ToString());
            itemWeightText.SetText(itemSO.Weight.ToString());
            if (type == TradeType.Buy)
            {
                itemPriceText.SetText(itemSO.BuyPrice.ToString());
                EnableSellButton(false);
            }
            else
            {
                itemPriceText.SetText(itemSO.SellPrice.ToString());
                EnableSellButton(true);
            }
            remainingQuanity = quantity;
            SetQuantity(quantity);
        }

        // call on invoking event
        private void ShowSelectedItemDetailPanel(bool value)
        {
            selectedItemPanel.SetActive(value);
        }

        // call on invoking event
        private void EnableSellButton(bool value)
        {
            buyItemButton.SetActive(!value);
            sellItemButton.SetActive(value);
        }

        private void ResetProperties()
        {
            currentSelectedQuantity = 0;
            remainingQuanity = 0;
            selectedItemDetails = null;
        }

        private void OnDisable()
        {
            EventService.Instance.OnClickItemFromList.RemoveListener(OnSelectItem);
        }

    }
}
