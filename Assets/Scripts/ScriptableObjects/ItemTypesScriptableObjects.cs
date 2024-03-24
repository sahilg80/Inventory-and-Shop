using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemTypeSO", menuName = "ScriptableObject/ItemTypeSO")]
    public class ItemTypesScriptableObjects : ScriptableObject
    {
        public ItemType Type;
        public List<ItemScriptableObject> ItemsList;
    }

}