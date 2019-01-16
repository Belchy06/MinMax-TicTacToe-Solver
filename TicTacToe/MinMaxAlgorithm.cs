using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class MinMaxAlgorithm
    {
        private GameLogic Logic;
        private Player BotPlayer;
        private long MoveBias;

        public MinMaxAlgorithm(GameLogic logic, Player player)
        {
            this.Logic = logic;
            this.BotPlayer = player;
        }

        private int CalculateMoveBias()
        {
            if (Logic.CheckWinState(Logic.GetGameBoard(), BotPlayer.GetCompetitorRole())) return -10;  // Competitor won

            else if (Logic.CheckWinState(Logic.GetGameBoard(), BotPlayer.GetRole())) return 10;    // Bot Player won

            else if (Logic.GetGameBoard().IsFull()) return 0;   // Draw

            return 0;
        }
    }
}