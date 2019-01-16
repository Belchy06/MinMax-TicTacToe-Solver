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
        private Player Player1;
        private Player Player2;

        private int n = 3; // Number of pieces in a row to win
        public static int moveCount = 0; // Number of moves that have been played

        public GameLogic(GameBoard board)
        {
            Board = board;
        }

        public GameLogic() { }

        public GameBoard GetGameBoard()
        {
            return Board;
        }

        public Player CreateNewPlayer(PlayerType type)
        {
            if(this.Player1 == null)
            {
                this.Player1 = type == PlayerType.HUMAN ? (Player)new HumanPlayer(this) : (Player)new BotPlayer(this);
                this.Player1.AssignRole(FieldState.X);
                return this.Player1;
            } else if(this.Player2 == null)
            {
                this.Player2 = type == PlayerType.HUMAN ? (Player) new HumanPlayer(this) : (Player) new BotPlayer(this);
                this.Player2.AssignRole(FieldState.O);
                return this.Player2;
            }
            return null;
        }

        public void DeletePlayer(FieldState player)
        {
            if (player == FieldState.X)
            {
                Player1 = null;
            } else if(player == FieldState.O)
            {
                Player2 = null;
            }
        }

        public Player GetPlayer1()
        {
            return Player1;
        }

        public Player GetPlayer2()
        {
            return Player2;
        }

        public void checkWinState(int moveX, int moveY, PlayerType playerType)
        {
            // This method is called at the end of a players turn. x and y are the position of the recently placed tile. whether it be from bot or player
            
            FieldState fieldToSearch = playerType == PlayerType.HUMAN ? Player1.GetRole() : Player2.GetRole(); // Determines which field to search the win conditions for

            // Check for a win based on the column of the move 
            for(int i = 0; i < n; i++)
            {
                if (Board.BoardState[moveX, i] != fieldToSearch) break;
                if (i == n - 1) ; // TODO report win for playerType
            }

            // Check for win based on the row of the move
            for(int i = 0; i < n; i++)
            {
                if (Board.BoardState[i, moveY] != fieldToSearch) break;
                if (i == n - 1) ; //TODO report win for playerType 
            }

            // Check for win diagonally
            if (moveX == moveY)
            {
                for(int i = 0; i < n; i++)
                {
                    if (Board.BoardState[i, i] != fieldToSearch) break;
                    if (i == n - 1) ; //TODO report win for playerType
                }
            }

            // Check for win anti-diagonally
            if (moveX + moveY == n - 1)
            {
                for(int i = 0; i < n; i++)
                {
                    if (Board.BoardState[i, ((n - 1) - i)] != fieldToSearch) break;
                    if (i == n - 1) ; //TODO report win for playerType
                }
            }

            // Check for draw
            if(moveCount == (Math.Pow(n, 2) - 1))
            {
                //TODO report draw
            }
        }

        public bool checkWinState(GameBoard board, PlayerType playerType)
        {
            // This method is called at the end of a players turn. x and y are the position of the recently placed tile. whether it be from bot or player

            FieldState fieldToSearch = playerType == PlayerType.HUMAN ? Player1.GetRole() : Player2.GetRole(); // Determines which field to search the win conditions for
            // Check for a win based on the column of the move
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    if (Board.BoardState[x, y] != fieldToSearch) break;
                    if (y == n - 1) return true;
                }
            }


            // Check for win based on the row of the move
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (Board.BoardState[x, y] != fieldToSearch) break;
                    if (y == n - 1) return true;
                }
            }

            // Check for win diagonally
            for (int i = 0; i < n; i++)
            {
                if (Board.BoardState[i, i] != fieldToSearch) break;
                if (i == n - 1) return true;
            }

            // Check for win anti-diagonally
            for (int i = 0; i < n; i++)
            {
                if (Board.BoardState[i, ((n - 1) - i)] != fieldToSearch) break;
                if (i == n - 1) return true;
            }

            // Check for draw
            if (moveCount == (Math.Pow(n, 2) - 1))
            {
                return false;
            }

            return false;
        }

    }

    [Flags]
    public enum PlayerType
    {
        HUMAN = 0,
        ROBOT = 1,
    }

}
