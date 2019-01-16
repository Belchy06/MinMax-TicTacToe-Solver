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
        }

        public override void RequestMove()
        {
            //TODO Use Min Max algorithm to determine best possible move
        }

    }
}
