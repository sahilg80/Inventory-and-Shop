using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObject/PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public float MaximumWeight;
        public float InitialAmout;
    }
}
