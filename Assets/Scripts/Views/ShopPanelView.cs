using Assets.Scripts.Controllers;
using Assets.Scripts.Services;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ShopPanelView : MonoBehaviour
    {
        private ShopController shopController;
        [SerializeField]
        private Transform itemsListContainer;

        private void OnEnable()
        {
        }
        private void Start()
        {
            
        }

        public void SetController(ShopController controller)
        {
            this.shopController = controller;
        }

        // call on invoking event
        //public void ShowItemsOfSelectedType(List<ItemCellView> itemsList)
        //{
        //    foreach (ItemCellView item in itemsList)
        //    {
        //        AddItem(item);
        //    }
        //}

        public void AddItem(ItemCellView itemView)
        {
            itemView.transform.parent = itemsListContainer;
            itemView.transform.localScale = Vector3.one;
        }

        //public void ClearItemsListFromUI()
        //{

        //}

        //private void OnSelectShopPanel()
        //{
        //    Debug.Log("selected shop panel ");
        //    shopController.OnSelectShopPanel();
        //}

        private void OnDisable()
        {

        }

    }
}
