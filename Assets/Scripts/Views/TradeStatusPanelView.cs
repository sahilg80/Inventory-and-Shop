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
        private const float ShowPanelDuration = 2f;
        private WaitForSeconds delay;
        private const string SoldText = "You sold";
        private const string BuyText = "You gained";

        private void OnEnable()
        {
            EventService.Instance.OnBuySelectedItem.AddListener(ShowBoughtItemDetialInUI);
            EventService.Instance.OnSoldSelectedItem.AddListener(ShowSoldItemDetailInUI);
            EventService.Instance.OnShowTradeStatus.AddListener(SetStatusText);
        }
        private void Start()
        {
            tradeStatusPanel.SetActive(false);
            delay = new WaitForSeconds(ShowPanelDuration);
        }

        // call on invoking event
        private void ShowBoughtItemDetialInUI(TradeDetail itemTraded)
        {
            string status = BuyText + " " + itemTraded.QuantityTraded + " " + itemTraded.ItemData.Name + "..";
            SetStatusText(status);
            StartCoroutine(ShowThisPanel());
        }

        private void ShowSoldItemDetailInUI(TradeDetail itemTraded)
        {
            string status = SoldText + " " + itemTraded.QuantityTraded + " " + itemTraded.ItemData.Name + "..";
            SetStatusText(status);
            StartCoroutine(ShowThisPanel());
        }

        private void SetStatusText(string status)
        {
            itemDetailText.SetText(status);
            StartCoroutine(ShowThisPanel());
        }

        IEnumerator ShowThisPanel()
        {
            tradeStatusPanel.SetActive(true);
            yield return delay;
            tradeStatusPanel.SetActive(false);
        }

        private void OnDisable()
        {
            EventService.Instance.OnBuySelectedItem.RemoveListener(ShowBoughtItemDetialInUI);
            EventService.Instance.OnSoldSelectedItem.RemoveListener(ShowSoldItemDetailInUI);
            EventService.Instance.OnShowTradeStatus.RemoveListener(SetStatusText);
        }
    }
}
