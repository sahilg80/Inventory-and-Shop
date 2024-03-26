using Assets.Scripts.Controllers;
using Assets.Scripts.Models;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class EventService
    {
        private static EventService instance;
        public static EventService Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventService();
                }
                return instance;
            }
        }

        public EventController OnSelectInventory { get; private set; }
        public EventController OnSelectShop { get; private set; }
        public EventController<ItemDetail, TradeType> OnClickItemFromList { get; private set; }
        public EventController<TradeDetail> OnBuySelectedItem { get; private set; }
        public EventController<TradeDetail> OnSoldSelectedItem { get; private set; }
        public EventController<string> OnShowTradeStatus { get; private set; }
        private EventService()
        {
            OnSelectInventory = new EventController();
            OnSelectShop = new EventController();
            OnClickItemFromList = new EventController<ItemDetail, TradeType>();
            OnBuySelectedItem = new EventController<TradeDetail>();
            OnSoldSelectedItem = new EventController<TradeDetail>();
            OnShowTradeStatus = new EventController<string>();
        }
    }
}
