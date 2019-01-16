using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class MinMaxAlgorithm
    {
        GameLogic logic = new GameLogic();
        private long moveBias;

        private int calculateMoveBias()
        {
            if (logic.checkWinState(new GameBoard(), PlayerType.HUMAN)) return -10;

            else if (logic.checkWinState(new GameBoard(), PlayerType.ROBOT)) return 10;

            else if (GameBoard.emptyCells.Count == 0) return 0;

            return 0;
        }
    }
}
