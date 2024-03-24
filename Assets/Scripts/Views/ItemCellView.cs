using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Views
{
    public class ItemCellView : MonoBehaviour
    {
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private TextMeshProUGUI quantityText;

        // Start is called before the first frame update
        private void OnEnable()
        {

        }

        public void SetProperties(int quantity, Sprite sprite)
        {
            itemIcon.sprite = sprite;
            quantityText.SetText(quantity.ToString());
        }

        private void OnDisable()
        {
            this.transform.parent = null;
        }
    }
}