using Assets.Scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
    public class ItemScriptableObject : ScriptableObject
    {
        // Define properties for the ScriptableObject
        public string ID;
        public string Name;
        public string Description;
        public Sprite Icon;
        public float BuyPrice;
        public float SellPrice;
        public ItemType Type;
        public float Weight;
        public int TotalQuantity;
        public RarityType Rarity;

        private void OnEnable()
        {
            ID = Guid.NewGuid().ToString();
        }
    }
}