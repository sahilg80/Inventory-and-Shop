using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using Assets.Scripts.Utilities;

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


        private void Start()
        {
            
        }


        // call on invoking event
        private void SetTradedItemDetail(TradeType type, string name, int quantity)
        {
            if(type == TradeType.Buy)
            {
                itemDetailText.SetText(buyText+" "+quantity + " " + name + "..");
            }
            else
            {
                itemDetailText.SetText(soldText + " " + quantity + " " + name + "..");
            }
            StartCoroutine(ShowThisPanel());
        }

        IEnumerator ShowThisPanel()
        {
            tradeStatusPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            tradeStatusPanel.SetActive(false);
        }

    }
}
