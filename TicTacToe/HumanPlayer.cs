using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TicTacToe
{
    class HumanPlayer : Player, BoardClickHandler
    {

        public HumanPlayer(GameLogic logic) : base(logic) {}


        public override void RequestMove()
        {
            this.Logic.GetGameBoard().SetClickHandler(this);    // Ready to listen to click events
        }

        public void ClickedOnBoard(int column, int row)
        {
            //GameBoard.emptyCells.Remove(new Point(column, row)); //Remove the newly populated cell from the list of empty cells
            //TODO Pass move decision onto GameLogic object
        }

    }
}