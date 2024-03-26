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
            EventService.Instance.OnBuySelectedItem.AddListener(ValidateRemainingQuantityOfTradedItem);
            EventService.Instance.OnSoldSelectedItem.AddListener(AddItemOnSelling);
            EventService.Instance.OnSelectGivenCategory.AddListener(OnSelectCategory);
        }

        ~ShopController()
        {
            EventService.Instance.OnSelectShop.RemoveListener(OnSelectShopPanel);
            EventService.Instance.OnBuySelectedItem.RemoveListener(ValidateRemainingQuantityOfTradedItem);
            EventService.Instance.OnSoldSelectedItem.RemoveListener(AddItemOnSelling);
            EventService.Instance.OnSelectGivenCategory.RemoveListener(OnSelectCategory);
        }

        private void OnSelectCategory(ItemType type)
        {
            if (GameService.Instance.CurrentTradeType != TradeType.Buy) return;

            ClearItemsListContainer();
            SetItemsListOfGivenCategory(type);
        }

        private void OnSelectShopPanel()
        {
            if (GameService.Instance.CurrentTradeType == TradeType.Buy) return;

            GameService.Instance.ClearItemsFromPreviousTradeType();
            GameService.Instance.ChangeTradeType(TradeType.Buy);
            SetItemsListFromShop();
        }

        private void ValidateRemainingQuantityOfTradedItem(TradeDetail itemBought)
        {
            ItemDetail item = shopModel.GetItemFromShopByID(itemBought.ItemData.ID);
            if (item == null) 
            {
                Debug.LogError("item not exist");
                return;
            }

            if (itemBought.RemainingQuantity <= 0)
            {
                //item.ItemCellView.UnParentThisObject();
                shopModel.RemoveItemFromShop(item.ItemData.ID);
                item.ItemCellView.RemoveListener();
                ObjectPoolManager.Instance.DeSpawnObject(item.ItemCellView.gameObject);
                item.ItemCellView = null;
            }
            else
            {
                item.QuantityAvaiableInCurrentTradeType = itemBought.RemainingQuantity;
                item.ItemCellView.SetQuantity(itemBought.RemainingQuantity);
            }
        }

        private void AddItemOnSelling(TradeDetail tradeDetail)
        {
            ItemDetail itemDetail = shopModel.GetItemFromShopByID(tradeDetail.ItemData.ID);
            if (itemDetail == null)
            {
                itemDetail = new ItemDetail()
                {
                    ItemData = tradeDetail.ItemData,
                    QuantityAvaiableInCurrentTradeType = tradeDetail.QuantityTraded,
                };

                shopModel.AddItemInShop(itemDetail, tradeDetail.ItemData.ID);
            }
            else
            {
                itemDetail.QuantityAvaiableInCurrentTradeType = itemDetail.QuantityAvaiableInCurrentTradeType + tradeDetail.QuantityTraded;
                shopModel.UpdateItemInShop(itemDetail, tradeDetail.ItemData.ID);
            }
        }

        public void ClearItemsListContainer()
        {
            // get list of all current items list and remove from their parent

            foreach (KeyValuePair<string, ItemDetail> pair in shopModel.ItemsExistInShop)
            {
                if (pair.Value.ItemCellView != null)
                {
                    pair.Value.ItemCellView.RemoveListener();
                    ObjectPoolManager.Instance.DeSpawnObject(pair.Value.ItemCellView.gameObject);
                    pair.Value.ItemCellView = null;
                }
            }
        }

        public void SetupShop(List<ItemTypesScriptableObjects> itemsList)
        {

            foreach (ItemTypesScriptableObjects itemsType in itemsList)
            {
                foreach(ItemScriptableObject item in itemsType.ItemsList)
                {
                    ItemDetail itemDetail = new ItemDetail()
                    {
                        ItemData = item,
                        QuantityAvaiableInCurrentTradeType = item.TotalQuantity,
                    };
                    shopModel.AddItemInShop(itemDetail, item.ID);
                }
            }

            SetItemsListFromShop();
        }

        private void SetItemsListFromShop()
        {
            // use shop model to get list

            foreach (KeyValuePair<string, ItemDetail> pair in shopModel.ItemsExistInShop)
            {
                ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                pair.Value.ItemCellView = itemCellView;
                itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                shopPanelView.AddItem(itemCellView);

                itemCellView.OnClickAddListener(() => {
                    // fire event and send item detail
                    EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Buy);
                });
            }
        }

        private void SetItemsListOfGivenCategory(ItemType type)
        {
            foreach (KeyValuePair<string, ItemDetail> pair in shopModel.ItemsExistInShop)
            {
                if (type == ItemType.All)
                {
                    ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                    pair.Value.ItemCellView = itemCellView;
                    itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                    itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                    shopPanelView.AddItem(itemCellView);

                    itemCellView.OnClickAddListener(() => {
                        // fire event and send item detail
                        EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Buy);
                    });
                }
                else if (pair.Value.ItemData.Type == type)
                {
                    ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                    pair.Value.ItemCellView = itemCellView;
                    itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                    itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                    shopPanelView.AddItem(itemCellView);

                    itemCellView.OnClickAddListener(() => {
                        // fire event and send item detail
                        EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Buy);
                    });
                }
            }
        }

    }
}
