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
        private ItemCellView itemCellView;
        [SerializeField]
        private PlayerView playerView;
        [SerializeField]
        private SoundView soundView;
        [SerializeField]
        private SelectedItemPanelView selectedItemPanelView;
        [SerializeField]
        private PlayerScriptableObject playerSO;
        [SerializeField]
        private List<ItemTypesScriptableObjects> itemsList;

        private PlayerInventoryController inventoryController;
        private ShopController shopController;
        private PlayerController playerController;

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
            playerController = new PlayerController(playerSO, playerView);
            inventoryController = new PlayerInventoryController(playerInventoryView, itemCellView);
            shopController = new ShopController(shopPanelView, itemCellView);
            SetupShop();
        }

        public void SetupShop()
        {
            ChangeTradeType(TradeType.Buy);
            shopController.SetupShop(itemsList);
            //EventService.Instance.OnSelectShop.InvokeEvent();
        }

        public void ChangeTradeType(TradeType type)
        {
            CurrentTradeType = type;
        }

        public void ClearItemsFromPreviousTradeType()
        {
            Debug.Log("select shop panel "+CurrentTradeType);
            if (CurrentTradeType == TradeType.Buy)
            {
                shopController.ClearItemsListContainer();
            }
            else if (CurrentTradeType == TradeType.Sell)
            {
                inventoryController.ClearItemsListContainer();
            }
        }

        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        public SoundView GetSoundView()
        {
            return soundView;
        }

        public SelectedItemPanelView GetSelectedItemPanel()
        {
            return selectedItemPanelView;
        }

    }
}
