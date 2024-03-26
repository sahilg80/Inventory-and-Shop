using Assets.Scripts.Controllers;
using Assets.Scripts.Services;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class PlayerInventoryView : MonoBehaviour
    {
        private PlayerInventoryController playerInventoryController;
        [SerializeField]
        private Transform itemsListContainer;

        private void OnEnable()
        {

        }

        private void Start()
        {

        }

        public void SetController(PlayerInventoryController controller)
        {
            playerInventoryController = controller;
        }

        //public void ShowItemsOfSelectedType(List<ItemCellView> itemsList)
        //{
        //    foreach(ItemCellView item in itemsList)
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

        // call on invoking event
        //private void OnSelectInventory()
        //{
        //    Debug.Log("selected player inventory " );
        //    playerInventoryController.OnSelectInventory();
        //}


        private void OnDisable()
        {

        }

    }
}
