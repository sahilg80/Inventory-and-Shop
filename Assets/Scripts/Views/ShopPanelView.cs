using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ShopPanelView : MonoBehaviour
    {

        private void Start()
        {
            
        }

        public void AddItem(ItemCellView itemView)
        {
            itemView.transform.parent = this.transform;
        }

        // call on invoking event
        private void ShowItemsOfSelectedType(ItemType type)
        {
            Debug.Log("selected type "+type);
        }

    }
}
