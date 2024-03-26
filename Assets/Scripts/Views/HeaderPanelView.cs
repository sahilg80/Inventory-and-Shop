using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.ScriptableObjects;

namespace Assets.Scripts.Views
{
    public class HeaderPanelView : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // call on invoking event
        //private void UpdatePlayerMoneyText(float money)
        //{
        //    playerMoneyText.SetText(money.ToString());
        //}

        //// call on invoking event
        //private void UpdatePlayerWeight(float weight)
        //{
        //    playerInventoryWeight.SetText(weight.ToString() + "/" + maximumWeight.ToString());
        //}

        public void OnSelectItemType(ItemTypesScriptableObjects itemTypeSO)
        {

        }
    }
}