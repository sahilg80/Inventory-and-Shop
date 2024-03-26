using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Views
{
    public class ItemCellView : MonoBehaviour
    {
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private TextMeshProUGUI quantityText;
        [SerializeField]
        private Button onClickItemButton;

        // Start is called before the first frame update
        private void OnEnable()
        {

        }

        public void OnClickAddListener(UnityAction action)
        {
            onClickItemButton.onClick.AddListener(action);
        }

        public void RemoveListener()
        {
            onClickItemButton.onClick.RemoveAllListeners();
        }

        public void SetImageIcon(Sprite sprite)
        {
            itemIcon.sprite = sprite;
        }

        public void SetQuantity(int quantity)
        {
            quantityText.SetText(quantity.ToString());

            // add handling if quantity reacehs to zero
        }

        public void UnParentThisObject()
        {
            this.transform.parent = null;
        }

        private void OnDisable()
        {

        }
    }
}