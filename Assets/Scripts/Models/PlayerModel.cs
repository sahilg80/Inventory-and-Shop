﻿using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class PlayerModel
    {
        public float CurrentOccupiedMoney;
        public float CurrentOccupiedWeight;
        public float MaximumWeight { get; private set; }
        public float InitialOccupiedAmount { get; private set; }

        public PlayerModel(PlayerScriptableObject playerSO)
        {
            InitialOccupiedAmount = playerSO.InitialAmout;
            MaximumWeight = playerSO.MaximumWeight;
        }
    }
}
