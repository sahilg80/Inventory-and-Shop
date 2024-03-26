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

        public void ShowItemsOfSelectedType(List<ItemCellView> itemsList)
        {
            foreach(ItemCellView item in itemsList)
            {
                AddItem(item);
            }
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

        private void AddItem(ItemCellView itemView)
        {
            itemView.transform.parent = this.transform;
        }

        private void OnDisable()
        {

        }

    }
}
