using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class BotPlayer : Player
    {
        MinMaxAlgorithm algorithm = new MinMaxAlgorithm();

        public BotPlayer(GameLogic logic) : base(logic) { }

        public override void RequestMove()
        {

            //TODO Use Min Max algorithm to determine best possible move
        }

    }
}
