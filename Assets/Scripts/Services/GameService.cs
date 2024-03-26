using Assets.Scripts.Controllers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utilities;
using Assets.Scripts.Views;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class GameService : MonoBehaviour
    {
        private static GameService instance;
        public static GameService Instance { get => instance; }
        
        [SerializeField]
        private PlayerInventoryView playerInventoryView;
        [SerializeField]
        private ShopPanelView shopPanelView;
        [SerializeField]
        private List<ItemTypesScriptableObjects> itemsList;
        [SerializeField]
        private ItemCellView itemCellView;
        [SerializeField]
        private PlayerView playerView;
        [SerializeField]
        private PlayerScriptableObject playerSO;

        private PlayerInventoryController inventoryController;
        private ShopController shopController;

        public TradeType CurrentTradeType { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PlayerController player = new PlayerController(playerSO, playerView);
            inventoryController = new PlayerInventoryController(playerInventoryView);
            shopController = new ShopController(shopPanelView, itemCellView);
            SetupShop();
        }

        public void SetupShop()
        {
            shopController.SetupShop(itemsList);
            //EventService.Instance.OnSelectShop.InvokeEvent();
        }

        public void ChangeTradeType(TradeType type)
        {
            CurrentTradeType = type;
        }

        public void ClearItemsFromPreviousTradeType()
        {
            if(CurrentTradeType == TradeType.Buy)
            {
                inventoryController.ClearItemsListContainer();
            }
            else if (CurrentTradeType == TradeType.Sell)
            {
                shopController.ClearItemsListContainer();
            }
        }
    }
}
