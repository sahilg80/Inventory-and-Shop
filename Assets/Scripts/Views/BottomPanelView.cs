using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class BottomPanelView : MonoBehaviour
    {
        private void Start()
        {
            
        }

        public void OnClickInventory()
        {
            Debug.Log("click inventory "+ GameService.Instance.CurrentTradeType);
            if (GameService.Instance.CurrentTradeType == Utilities.TradeType.Sell) return;
            EventService.Instance.OnSelectInventory.InvokeEvent();
        }

        public void OnClickShop()
        {
            Debug.Log("click shop "+ GameService.Instance.CurrentTradeType);
            if (GameService.Instance.CurrentTradeType == Utilities.TradeType.Buy) return;
            EventService.Instance.OnSelectShop.InvokeEvent();
        }
    }
}
