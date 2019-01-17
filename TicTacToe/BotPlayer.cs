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
        GameBoard Board;

        public BotPlayer(GameLogic logic, GameBoard board) : base(logic) {
            this.Algorithm = new MinMaxAlgorithm(logic, this, board);
            this.Board = board;
            this.PlayerType = PlayerType.ROBOT;
        }


        public override void RequestMove()
        {
            Algorithm.PerformMove(Board);
            //TODO Use Min Max algorithm to determine best possible move
        }

    }
}
