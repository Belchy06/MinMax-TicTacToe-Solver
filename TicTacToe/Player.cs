using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    abstract class Player
    {
        protected FieldState PlayerFigure;    // Defines whether player places Xs or Os
        protected GameLogic Logic;

        public Player(ref GameLogic logic, FieldState figure)
        {
            this.PlayerFigure = figure;
            this.Logic = logic;
        }

        public abstract void RequestMove();

    }
}
