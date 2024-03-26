using Assets.Scripts.Models;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public class PlayerController
    {
        public PlayerController(PlayerScriptableObject playerSO, PlayerView playerView)
        {
            PlayerModel model = new PlayerModel(playerSO);
            playerView.SetInitialPlayerData(playerSO.InitialAmout, playerSO.MaximumWeight);
        }

        public void UpdateWeight()
        {

        }

        public void UpdateMoney()
        {

        }
    }
}
