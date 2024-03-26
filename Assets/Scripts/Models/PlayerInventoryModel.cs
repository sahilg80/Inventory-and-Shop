using Assets.Scripts.Controllers;
using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class PlayerInventoryModel
    {
        private PlayerInventoryController playerInventoryController;
        public ItemType CurrentSelectedType { get; private set; }

        public PlayerInventoryModel(PlayerInventoryController controller)
        {
            playerInventoryController = controller;
            CurrentSelectedType = ItemType.Consumable;
        }
    }
}
