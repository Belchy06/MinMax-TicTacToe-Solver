using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class BotPlayer : Player
    {

        public BotPlayer(ref GameLogic logic, FieldState figure) : base(ref logic, figure) { }

        public override void RequestMove()
        {
            //TODO Use Min Max algorithm to determine best possible move
        }

    }
}
