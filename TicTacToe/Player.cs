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

        public Player(GameLogic logic)
        {
            this.Logic = logic;
        }

        public void AssignRole(FieldState playerFigure)
        {
            this.PlayerFigure = playerFigure;
        }

        public FieldState GetRole()
        {
            return this.PlayerFigure;
        }

        public GameLogic GetGameLogic()
        {
            return this.Logic;
        }

        public void SetGameLogic(GameLogic logic)
        {
            this.Logic = logic;
        }

        public abstract void RequestMove();

    }
}
