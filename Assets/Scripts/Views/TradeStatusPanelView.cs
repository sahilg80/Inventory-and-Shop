using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using Assets.Scripts.Utilities;
using Assets.Scripts.Services;
using Assets.Scripts.Models;

namespace Assets.Scripts.Views
{
    public class TradeStatusPanelView : MonoBehaviour
    {
        [SerializeField]
        private GameObject tradeStatusPanel;
        [SerializeField]
        private TextMeshProUGUI itemDetailText;
        private const string soldText = "You sold";
        private const string buyText = "You gained";

        private void OnEnable()
        {
            EventService.Instance.OnBuySelectedItem.AddListener(SetTradedItemDetail);
            EventService.Instance.OnSoldSelectedItem.AddListener(SetTradedItemDetail);
        }
        private void Start()
        {
            tradeStatusPanel.SetActive(false);
        }


        // call on invoking event
        private void SetTradedItemDetail(ItemDetail itemTraded, TradeType type)
        {
            if(type == TradeType.Buy)
            {
                itemDetailText.SetText(buyText+" "+ itemTraded.QuantityAvaiableInCurrentTradeType + " " + itemTraded.ItemData.Name + "..");
            }
            else
            {
                itemDetailText.SetText(soldText + " " + itemTraded.QuantityAvaiableInCurrentTradeType + " " + itemTraded.ItemData.Name + "..");
            }
            StartCoroutine(ShowThisPanel());
        }

        IEnumerator ShowThisPanel()
        {
            tradeStatusPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            tradeStatusPanel.SetActive(false);
        }

        private void OnDisable()
        {
            EventService.Instance.OnBuySelectedItem.RemoveListener(SetTradedItemDetail);
            EventService.Instance.OnSoldSelectedItem.RemoveListener(SetTradedItemDetail);
        }
    }
}
