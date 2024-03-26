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
        private TextMeshProUGUI itemQuanityText;
        [SerializeField]
        private GameObject buyItemButton;
        [SerializeField]
        private GameObject sellItemButton;

        private int currentSelectedQuantity;
        private int remainingQuanity;
        private ItemDetail selectedItemDetails;

        private void OnEnable()
        {
            EventService.Instance.OnClickItemFromList.AddListener(OnSelectItem);
        }

        private void Start()
        {
            ShowSelectedItemDetailPanel(false);
        }


        //public void SetItemIcon(Sprite sprite)
        //{
        //    itemIcon.sprite = sprite;
        //}

        //public void SetItemName(string name)
        //{
        //    itemNameText.SetText(name);
        //}

        //public void SetItemType(ItemType type)
        //{
        //    itemTypeText.SetText(type.ToString());
        //}

        //public void SetItemDescription(string description)
        //{
        //    itemDescriptionText.SetText(description);
        //}

        //public void SetRarityType(RarityType type)
        //{
        //    itemRarityTypeText.SetText(type.ToString());
        //}

        //public void SetPrice(float price)
        //{
        //    itemPriceText.SetText(price.ToString());
        //}

        //public void SetWeight(float weight)
        //{
        //    itemWeightText.SetText(weight.ToString());
        //}

        public void SetQuantity(int quantity)
        {
            currentSelectedQuantity = quantity;
            itemQuanityText.SetText(quantity.ToString());
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
            ItemDetail itemBoughtDetail = new ItemDetail()
            {
                ItemData = selectedItemDetails.ItemData,
                QuantityAvaiableInCurrentTradeType = currentSelectedQuantity,
            };
            
            selectedItemDetails.QuantityAvaiableInCurrentTradeType = selectedItemDetails.QuantityAvaiableInCurrentTradeType - currentSelectedQuantity;

            EventService.Instance.OnBuySelectedItem.InvokeEvent(itemBoughtDetail, TradeType.Buy);
        }

        public void OnClickSellButton()
        {
            Debug.Log("sell this item ");
            ItemDetail itemSoldDetail = new ItemDetail()
            {
                ItemData = selectedItemDetails.ItemData,
                QuantityAvaiableInCurrentTradeType = currentSelectedQuantity,
            };

            selectedItemDetails.QuantityAvaiableInCurrentTradeType = selectedItemDetails.QuantityAvaiableInCurrentTradeType - currentSelectedQuantity;

            EventService.Instance.OnSoldSelectedItem.InvokeEvent(selectedItemDetails, TradeType.Sell);
        }

        private void OnSelectItem(ItemDetail item, TradeType type)
        {
            selectedItemDetails = item;
            SetParameters(item.ItemData, item.QuantityAvaiableInCurrentTradeType, type);
            ShowSelectedItemDetailPanel(true);
        }

        private void SetParameters(ItemScriptableObject itemSO, int quanity, TradeType type)
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
            remainingQuanity = quanity;
            SetQuantity(quanity);
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

        private void OnDisable()
        {
            EventService.Instance.OnClickItemFromList.RemoveListener(OnSelectItem);
        }

    }
}
