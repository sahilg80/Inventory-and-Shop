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
            EventService.Instance.OnBuySelectedItem.AddListener(ValidateTradedItemRemainingQuantity);
        }

        ~ShopController()
        {
            EventService.Instance.OnSelectShop.RemoveListener(OnSelectShopPanel);
            EventService.Instance.OnBuySelectedItem.RemoveListener(ValidateTradedItemRemainingQuantity);
        }

        private void OnSelectShopPanel()
        {
            GameService.Instance.ClearItemsFromPreviousTradeType();
            GameService.Instance.ChangeTradeType(TradeType.Buy);
            SetItemsListFromShop();
        }

        private void ValidateTradedItemRemainingQuantity(TradeDetail itemBought)
        {
            ItemDetail item = shopModel.GetItemFromShopByID(itemBought.ItemData.ID);
            if (item == null) 
            {
                Debug.LogError("item not exist");
                return;
            }

            if (itemBought.RemainingQuantity <= 0)
            {
                item.ItemCellView.UnParentThisObject();
                ObjectPoolManager.Instance.DeSpawnObject(item.ItemCellView.gameObject);
                item.ItemCellView = null;
            }
            else
            {
                item.QuantityAvaiableInCurrentTradeType = itemBought.RemainingQuantity;
                item.ItemCellView.SetQuantity(itemBought.RemainingQuantity);
            }
        }

        public void ClearItemsListContainer()
        {
            // get list of all current items list and remove from their parent

            foreach (KeyValuePair<string, ItemDetail> pair in shopModel.ItemsExistInShop)
            {
                pair.Value.ItemCellView.UnParentThisObject();
                ObjectPoolManager.Instance.DeSpawnObject(pair.Value.ItemCellView.gameObject);
                pair.Value.ItemCellView = null;
            }

        }

        public void SetupShop(List<ItemTypesScriptableObjects> itemsList)
        {
            Dictionary<string, ItemDetail> dictionary = new Dictionary<string, ItemDetail>();

            foreach (ItemTypesScriptableObjects itemsType in itemsList)
            {
                foreach(ItemScriptableObject item in itemsType.ItemsList)
                {
                    ItemDetail itemDetail = new ItemDetail()
                    {
                        ItemData = item,
                        QuantityAvaiableInCurrentTradeType = item.TotalQuantity,
                    };

                    dictionary.Add(item.ID, itemDetail);
                }
            }

            shopModel.SetItemsInShop(dictionary);
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
    }
}
