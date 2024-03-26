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

        public void SetProperties(int quantity, Sprite sprite)
        {
            itemIcon.sprite = sprite;
            quantityText.SetText(quantity.ToString());
        }

        public void UnParentChild()
        {
            this.transform.parent = null;
        }

        private void OnDisable()
        {

        }
    }
}