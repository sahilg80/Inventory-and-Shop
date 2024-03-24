using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Utilities;
using UnityEngine.UI;

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

        private void Start()
        {
            ShowThisPanel(false);
        }

        public void SetItemIcon(Sprite sprite)
        {
            itemIcon.sprite = sprite;
        }

        public void SetItemName(string name)
        {
            itemNameText.SetText(name);
        }

        public void SetItemType(ItemType type)
        {
            itemTypeText.SetText(type.ToString());
        }

        public void SetItemDescription(string description)
        {
            itemDescriptionText.SetText(description);
        }

        public void SetRarityType(RarityType type)
        {
            itemRarityTypeText.SetText(type.ToString());
        }

        public void SetPrice(float price)
        {
            itemPriceText.SetText(price.ToString());
        }

        public void SetWeight(float weight)
        {
            itemWeightText.SetText(weight.ToString());
        }

        public void SetQuantity(int quantity)
        {
            currentSelectedQuantity = quantity;
            itemQuanityText.SetText(quantity.ToString());
        }

        public void OnClickIncreaseQuantity()
        {
            SetQuantity(currentSelectedQuantity++);
        }

        public void OnClickDecreaseQuantity()
        {
            SetQuantity(currentSelectedQuantity--);
        }

        public void OnClickBuyButton()
        {
            Debug.Log("buy this item ");
        }

        public void OnClickSellButton()
        {
            Debug.Log("sell this item ");
        }

        // call on invoking event
        private void ShowThisPanel(bool value)
        {
            selectedItemPanel.SetActive(value);
        }

        // call on invoking event
        private void EnableSellButton(bool value)
        {
            buyItemButton.SetActive(!value);
            sellItemButton.SetActive(value);
        }

    }
}
