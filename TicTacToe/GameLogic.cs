using System;
using System.Windows.Forms;
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

        private readonly int N = 3; // Number of pieces in a row to win
        private PlayerTurn CurrentTurn; // Describes which player's turn it is

        public GameLogic(GameBoard board)
        {
            Board = board;
        }

        public GameBoard GetGameBoard()
        {
            return Board;
        }

        public void StartGame()
        {
            this.CurrentTurn = PlayerTurn.PlayerOne;   // Player1 begins
            MessageBox.Show("It's your turn, Player!");
            Player1.RequestMove();  // Tell Player that it's his time to play!
        }

        public Player CreateNewPlayer(PlayerType type)
        {
            if(this.Player1 == null)
            {
                this.Player1 = type == PlayerType.HUMAN ? (Player)new HumanPlayer(this) : (Player)new BotPlayer(this, Board);
                this.Player1.AssignRole(FieldState.X);
                return this.Player1;
            } else if(this.Player2 == null)
            {
                this.Player2 = type == PlayerType.HUMAN ? (Player)new HumanPlayer(this) : (Player)new BotPlayer(this, Board);
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

        public void MakeMove(int column, int row, Player player)
        {
            FieldState Field = player.GetRole();
            Board.SetField(Field, column, row);
            Board.Refresh();
            CheckWinState(column, row, Field);
            Update();                           
        }

        public void Update()
        {
            if (CurrentTurn == PlayerTurn.PlayerOne)
            {
                CurrentTurn = PlayerTurn.PlayerTwo;
                Player2.RequestMove();
            }
            else
            {
                CurrentTurn = PlayerTurn.PlayerOne;
                Player1.RequestMove();            
            }
            
        }

        /**
         * This method is called at the end of a players turn. x and y are the position of the recently placed tile. whether it be from bot or player
         */
        public void CheckWinState(int moveX, int moveY, FieldState fieldToSearch)
        {

            /**
             *  COMMENT
             *  I don't think it's necessary to provide a property 'N' that describes the number of pieces in a row to win, because
             *  in Tic Tac Toe games that is usually equal to the number of rows and columns.
             *  However, if we want to do that we have to consider that the last fields in a row will not be checked,
             *  if the number of pieces needed, is lower than the fields in a row. So if, lets say, N equals 2, but it's
             *  a 3x3 grid, the loop will alredy break at the first column in the row, even if the next two columns both have
             *  the correct field state. So we either have to fix both methods or just loop through the entire row to begin with 
             *  and scrap the 'N' property
             *  - Sam
             */
           
            // Check for a win based on the column of the move 
            for(int i = 0; i < N; i++)
            {
                if (Board.BoardState[moveX, i] != fieldToSearch) break;
                if (i == N - 1)
                {
                    if(fieldToSearch == FieldState.X)
                    {
                        MessageBox.Show("Player 1 Wins!");
                        Application.Restart();
                    } else
                    {
                        MessageBox.Show("AI Wins!");
                        Application.Restart();
                    }
                } 
            }

            // Check for win based on the row of the move
            for(int i = 0; i < N; i++)
            {
                if (Board.BoardState[i, moveY] != fieldToSearch) break;
                if (i == N - 1)
                {
                    if (fieldToSearch == FieldState.X)
                    {
                        MessageBox.Show("Player 1 Wins!");
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("AI Wins!");
                        Application.Restart();
                    }
                }
            }

            // Check for win diagonally
            if (moveX == moveY)
            {
                for(int i = 0; i < N; i++)
                {
                    if (Board.BoardState[i, i] != fieldToSearch) break;
                    if (i == N - 1)
                    {
                        if (fieldToSearch == FieldState.X)
                        {
                            MessageBox.Show("Player 1 Wins!");
                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show("AI Wins!");
                            Application.Restart();
                        }
                    }
                }
            }

            // Check for win anti-diagonally
            if (moveX + moveY == N - 1)
            {
                for(int i = 0; i < N; i++)
                {
                    if (Board.BoardState[i, ((N - 1) - i)] != fieldToSearch) break;
                    if (i == N - 1)
                    {
                        if (fieldToSearch == FieldState.X)
                        {
                            MessageBox.Show("Player 1 Wins!");
                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show("AI Wins!");
                            Application.Restart();
                        }
                    }
                }
            }

            // Check for draw
            if (Board.IsFull())
            {
                MessageBox.Show("DRAW");
                Application.Restart();
            }
        }

        /**
         * This method is called at the end of a players turn. x and y are the position of the recently placed tile. whether it be from bot or player
         */
        public EndState CheckWinState(GameBoard board, FieldState fieldToSearch)
        {
            EndState end;
            if(fieldToSearch == FieldState.X)
            {
                end = EndState.XWin;
            } else
            {
                end = EndState.OWin;
            }
            // Check for a win based on the column of the move
            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    if (Board.BoardState[x, y] != fieldToSearch) break;
                    if (y == N - 1) return end;
                }
            }


            // Check for win based on the row of the move
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    if (Board.BoardState[x, y] != fieldToSearch) break;
                    if (x == N - 1) return end;
                }
            }

            // Check for win diagonally
            for (int i = 0; i < N; i++)
            {
                if (Board.BoardState[i, i] != fieldToSearch) break;
                if (i == N - 1) return end;
            }

            // Check for win anti-diagonally
            for (int i = 0; i < N; i++)
            {
                if (Board.BoardState[i, ((N - 1) - i)] != fieldToSearch) break;
                if (i == N - 1) return end;
            }

            // Check for draw
            if(board.IsFull())
            {
                return EndState.Draw;
            }

            return EndState.Other;
        }

    }

    [Flags]
    public enum PlayerType
    {
        HUMAN = 0,
        ROBOT = 1,
    }

    public enum PlayerTurn
    {
        PlayerOne = 1,
        PlayerTwo = 2,
    }

}