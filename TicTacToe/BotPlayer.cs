using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class BotPlayer : Player
    {
        MinMaxAlgorithm Algorithm;

        public BotPlayer(GameLogic logic) : base(logic) {
            this.Algorithm = new MinMaxAlgorithm(logic, this);
            this.PlayerType = PlayerType.ROBOT;
        }

        public override void RequestMove()
        {
            Algorithm.PerformMove();
        }

    }
}
