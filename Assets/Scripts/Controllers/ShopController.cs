using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services;
using Assets.Scripts.Utilities;
using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ShopController
    {
        private ShopModel shopModel;
        private ShopPanelView shopPanelView;
        private ItemCellView itemCellViewPrefab;

        public ShopController(ShopPanelView shopPanelView, ItemCellView itemCellView)
        {
            this.shopPanelView = shopPanelView;
            this.shopPanelView.SetController(this);
            shopModel = new ShopModel(this);
            this.itemCellViewPrefab = itemCellView;
            EventService.Instance.OnSelectShop.AddListener(OnSelectShopPanel);
            EventService.Instance.OnBuySelectedItem.AddListener(OnBuySelectedItem);
        }

        ~ShopController()
        {
            EventService.Instance.OnSelectShop.RemoveListener(OnSelectShopPanel);
            EventService.Instance.OnBuySelectedItem.RemoveListener(OnBuySelectedItem);
        }

        private void OnSelectShopPanel()
        {
            GameService.Instance.ClearItemsFromPreviousTradeType();
            GameService.Instance.ChangeTradeType(TradeType.Buy);
            SetItemsListFromShop();
        }

        private void OnBuySelectedItem(ItemDetail itemBought, TradeType type)
        {
            if (type != TradeType.Buy) return;
            Debug.Log("buying this item ");
            // update in player inventory with item
        }

        public void ClearItemsListContainer()
        {
            // get list of all current items list and remove from their parent
            foreach (ItemDetail item in shopModel.CurrentItemsList)
            {
                item.ItemCellView.UnParentChild();
                ObjectPoolManager.Instance.DeSpawnObject(item.ItemCellView.gameObject);
                item.ItemCellView = null;
            }
        }

        public void SetupShop(List<ItemTypesScriptableObjects> itemsList)
        {
            List<ItemDetail> currentShopItemsList = new List<ItemDetail>();

            foreach (ItemTypesScriptableObjects itemsType in itemsList)
            {
                foreach(ItemScriptableObject item in itemsType.ItemsList)
                {
                    ItemDetail itemDetail = new ItemDetail()
                    {
                        ItemData = item,
                        QuantityAvaiableInCurrentTradeType = item.TotalQuantity,
                    };

                    currentShopItemsList.Add(itemDetail);
                }
            }
            shopModel.SetCurrentItemsList(currentShopItemsList);
            SetItemsListFromShop();
        }

        private void SetItemsListFromShop()
        {
            // use shop model to get list
            foreach(ItemDetail item in shopModel.CurrentItemsList)
            {
                ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                item.ItemCellView = itemCellView;
                itemCellView.SetProperties(item.QuantityAvaiableInCurrentTradeType, item.ItemData.Icon);
                shopPanelView.AddItem(itemCellView);

                itemCellView.OnClickAddListener(()=> {
                    // fire event and send item detail
                    Debug.Log("item detail "+item.ItemData.name);
                    EventService.Instance.OnClickItemFromList.InvokeEvent(item, TradeType.Buy);
                });
            }
        }
    }
}
