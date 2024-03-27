using Assets.Scripts.Models;
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
    public class PlayerInventoryController
    {
        private PlayerInventoryView playerInventoryView;
        private PlayerInventoryModel playerInventoryModel;
        private ItemCellView itemCellViewPrefab;

        public PlayerInventoryController(PlayerInventoryView playerInventoryView, ItemCellView itemCellView)
        {
            this.playerInventoryView = playerInventoryView;
            this.playerInventoryView.SetController(this);
            this.playerInventoryModel = new PlayerInventoryModel(this);
            this.itemCellViewPrefab = itemCellView;
            EventService.Instance.OnSelectInventory.AddListener(OnSelectInventoryPanel);
            EventService.Instance.OnBuySelectedItem.AddListener(AddItemOnBuying);
            EventService.Instance.OnSoldSelectedItem.AddListener(ValidateRemainingQuantityOfTradedItem);
            EventService.Instance.OnSelectGivenCategory.AddListener(OnSelectCategory);
        }

        ~PlayerInventoryController()
        {
            EventService.Instance.OnSelectInventory.RemoveListener(OnSelectInventoryPanel);
            EventService.Instance.OnBuySelectedItem.RemoveListener(AddItemOnBuying);
            EventService.Instance.OnSoldSelectedItem.RemoveListener(ValidateRemainingQuantityOfTradedItem);
            EventService.Instance.OnSelectGivenCategory.RemoveListener(OnSelectCategory);
        }

        private void OnSelectCategory(ItemType type)
        {
            if (GameService.Instance.CurrentTradeType != TradeType.Sell) return;

            ClearItemsListContainer();
            SetItemsListOfGivenCategory(type);
        }

        private void OnSelectInventoryPanel()
        {
            if (GameService.Instance.CurrentTradeType == TradeType.Sell) return;

            GameService.Instance.ClearItemsFromPreviousTradeType();
            GameService.Instance.ChangeTradeType(TradeType.Sell);
            SetItemsListFromInventory();
        }

        public void ClearItemsListContainer()
        {
            // get list of all current items list and remove from their parent

            foreach (KeyValuePair<string, ItemDetail> pair in playerInventoryModel.ItemsExistInInventory)
            {
                if (pair.Value.ItemCellView != null)
                {
                    pair.Value.ItemCellView.RemoveListener();
                    ObjectPoolManager.Instance.DeSpawnObject(pair.Value.ItemCellView.gameObject);
                    pair.Value.ItemCellView = null;
                }
            }
        }

        private void ValidateRemainingQuantityOfTradedItem(TradeDetail itemSold)
        {
            ItemDetail item = playerInventoryModel.GetItemFromInventoryByID(itemSold.ItemData.ID);
            if (item == null)
            {
                Debug.LogError("item not exist");
                return;
            }

            if (itemSold.RemainingQuantity <= 0)
            {
                playerInventoryModel.RemoveItemFromShop(item.ItemData.ID);
                item.ItemCellView.RemoveListener();
                ObjectPoolManager.Instance.DeSpawnObject(item.ItemCellView.gameObject);
                item.ItemCellView = null;
            }
            else
            {
                item.QuantityAvaiableInCurrentTradeType = itemSold.RemainingQuantity;
                item.ItemCellView.SetQuantity(itemSold.RemainingQuantity);
            }
        }

        private void AddItemOnBuying(TradeDetail tradeDetail)
        {
            ItemDetail itemDetail =  playerInventoryModel.GetItemFromInventoryByID(tradeDetail.ItemData.ID);
            if (itemDetail == null) 
            {
                itemDetail = new ItemDetail()
                {
                    ItemData = tradeDetail.ItemData,
                    QuantityAvaiableInCurrentTradeType = tradeDetail.QuantityTraded,
                };

                playerInventoryModel.AddItemInInventory(itemDetail, tradeDetail.ItemData.ID); 
            }
            else
            {
                itemDetail.QuantityAvaiableInCurrentTradeType = itemDetail.QuantityAvaiableInCurrentTradeType + tradeDetail.QuantityTraded;
                playerInventoryModel.UpdateItemInInventory(itemDetail, tradeDetail.ItemData.ID);
            }
        }

        private void SetItemsListFromInventory()
        {
            // use shop model to get list

            foreach (KeyValuePair<string, ItemDetail> pair in playerInventoryModel.ItemsExistInInventory)
            {
                ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                pair.Value.ItemCellView = itemCellView;
                itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                playerInventoryView.AddItem(itemCellView);

                itemCellView.OnClickAddListener(() => {
                    // fire event and send item detail
                    GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.OnClick);
                    EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Sell);
                });
            }
        }

        private void SetItemsListOfGivenCategory(ItemType type)
        {
            foreach (KeyValuePair<string, ItemDetail> pair in playerInventoryModel.ItemsExistInInventory)
            {
                if (type == ItemType.All)
                {
                    ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                    pair.Value.ItemCellView = itemCellView;
                    itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                    itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                    playerInventoryView.AddItem(itemCellView);

                    itemCellView.OnClickAddListener(() => {
                        // fire event and send item detail
                        EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Sell);
                    });
                }
                else if (pair.Value.ItemData.Type == type)
                {
                    ItemCellView itemCellView = ObjectPoolManager.Instance.SpawnObject<ItemCellView>(itemCellViewPrefab);
                    pair.Value.ItemCellView = itemCellView;
                    itemCellView.SetImageIcon(pair.Value.ItemData.Icon);
                    itemCellView.SetQuantity(pair.Value.QuantityAvaiableInCurrentTradeType);
                    playerInventoryView.AddItem(itemCellView);

                    itemCellView.OnClickAddListener(() => {
                        // fire event and send item detail
                        EventService.Instance.OnClickItemFromList.InvokeEvent(pair.Value, TradeType.Sell);
                    });
                }
            }
        }
    }
}
