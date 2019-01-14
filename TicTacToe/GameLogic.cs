using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class GameLogic
    {
        private GameBoard Board;

        public GameLogic(GameBoard board)
        {
            this.Board = board;
        }

        public GameBoard GetGameBoard()
        {
            return Board;
        }

    }
}
