using Assets.Scripts.Controllers;
using Assets.Scripts.ScriptableObjects;
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
        public List<ItemDetail> CurrentItemsList { get; private set; }
        public ShopModel(ShopController shopController)
        {
            this.shopController = shopController;
        }

        public void SetCurrentItemsList(List<ItemDetail> list)
        {
            CurrentItemsList = list;
        }
    }

    public class ItemDetail
    {
        public ItemScriptableObject ItemData;
        public int QuantityAvaiableInCurrentTradeType;
        public ItemCellView ItemCellView;
    }
}
