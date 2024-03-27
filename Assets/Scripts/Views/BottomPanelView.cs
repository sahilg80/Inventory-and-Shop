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
            GameService.Instance.GetSoundView().PlaySoundEffects(Utilities.SoundType.OnClick);
            if (GameService.Instance.CurrentTradeType == Utilities.TradeType.Sell) return;
            EventService.Instance.OnSelectInventory.InvokeEvent();
            GameService.Instance.GetSelectedItemPanel().ShowSelectedItemDetailPanel(false);
        }

        public void OnClickShop()
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Utilities.SoundType.OnClick);
            if (GameService.Instance.CurrentTradeType == Utilities.TradeType.Buy) return;
            EventService.Instance.OnSelectShop.InvokeEvent();
            GameService.Instance.GetSelectedItemPanel().ShowSelectedItemDetailPanel(false);
        }
    }
}
