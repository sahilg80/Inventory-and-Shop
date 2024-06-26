using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services;

namespace Assets.Scripts.Views
{
    public class HeaderPanelView : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnSelectItemType(ItemTypesScriptableObjects itemTypeSO)
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Utilities.SoundType.OnClick);
            EventService.Instance.OnSelectGivenCategory.InvokeEvent(itemTypeSO.Type);
            GameService.Instance.GetSelectedItemPanel().ShowSelectedItemDetailPanel(false);
        }
    }
}