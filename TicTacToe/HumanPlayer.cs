using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    class HumanPlayer : Player, BoardClickHandler
    {

        public HumanPlayer(GameLogic logic) : base(logic)
        {
            this.PlayerType = PlayerType.HUMAN;
        }


        public override void RequestMove()
        {
            this.Logic.GetGameBoard().SetClickHandler(this);    // Ready to listen to click events          
        }

        public void ClickedOnBoard(int column, int row)
        {
            Logic.MakeMove(column, row, this);
        }

    }
}