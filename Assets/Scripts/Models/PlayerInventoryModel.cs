using Assets.Scripts.Controllers;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class PlayerInventoryModel
    {
        private PlayerInventoryController playerInventoryController;
        public ItemType CurrentSelectedType { get; private set; }
        public Dictionary<string, ItemDetail> ItemsExistInInventory { get; private set; }

        public PlayerInventoryModel(PlayerInventoryController controller)
        {
            playerInventoryController = controller;
            CurrentSelectedType = ItemType.Consumable;
            ItemsExistInInventory = new Dictionary<string, ItemDetail>();
        }

        //public void SetItemsInInventory(Dictionary<string, ItemDetail> list)
        //{
        //    ItemsExistInInventory = list;
        //}

        public ItemDetail GetItemFromInventoryByID(string id)
        {
            ItemDetail foundItem;
            ItemsExistInInventory.TryGetValue(id, out foundItem);
            return foundItem;
        }

        public void AddItemInInventory(ItemDetail detail, string id)
        {
            ItemsExistInInventory.Add(id, detail);
        }

        public void UpdateItemInInventory(ItemDetail detail, string id)
        {
            ItemsExistInInventory[id] = detail;
        }

        public void RemoveItemFromShop(string id)
        {
            ItemsExistInInventory.Remove(id);
        }

    }
}
