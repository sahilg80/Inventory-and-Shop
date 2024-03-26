using Assets.Scripts.Models;
using Assets.Scripts.Services;
using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public class PlayerInventoryController
    {
        private PlayerInventoryView playerInventoryView;
        private PlayerInventoryModel playerInventoryModel;

        public PlayerInventoryController(PlayerInventoryView playerInventoryView)
        {
            this.playerInventoryView = playerInventoryView;
            this.playerInventoryView.SetController(this);
            this.playerInventoryModel = new PlayerInventoryModel(this);
            EventService.Instance.OnSelectInventory.AddListener(OnSelectInventory);
        }

        ~PlayerInventoryController()
        {
            EventService.Instance.OnSelectInventory.RemoveListener(OnSelectInventory);
        }

        private void OnSelectInventory()
        {
            GameService.Instance.ClearItemsFromPreviousTradeType();
            GameService.Instance.ChangeTradeType(Utilities.TradeType.Sell);
            GetItemsListFromInventory();
        }

        public void ClearItemsListContainer()
        {
            // get list of all current items list and remove from their parent

        }

        private void GetItemsListFromInventory()
        {
            // use player model to get list
        }
    }
}
