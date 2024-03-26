using Assets.Scripts.Controllers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utilities;
using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class ShopModel
    {
        private ShopController shopController;
        public Dictionary<string, ItemDetail> ItemsExistInShop { get; private set; }
        //public List<ItemDetail> CurrentItemsList { get; private set; }
        public ShopModel(ShopController shopController)
        {
            this.shopController = shopController;
        }

        //public void SetCurrentItemsList(List<ItemDetail> list)
        //{
        //    CurrentItemsList = list;
        //}

        public void SetItemsInShop(Dictionary<string, ItemDetail> list)
        {
            ItemsExistInShop = list;
        }

        public ItemDetail GetItemFromShopByID(string id)
        {
            ItemDetail foundItem;
            ItemsExistInShop.TryGetValue(id, out foundItem);
            return foundItem;
        }
    }

    public class ItemDetail
    {
        public ItemScriptableObject ItemData;
        public int QuantityAvaiableInCurrentTradeType;
        public ItemCellView ItemCellView;
    }

    public class TradeDetail
    {
        public ItemScriptableObject ItemData;
        public float QuantityTraded;
        public float TradedAmount;
        public float TradedWeight;
        public int RemainingQuantity;
    }
}
