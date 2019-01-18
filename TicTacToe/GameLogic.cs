using System;
using System.Windows.Forms;

namespace TicTacToe
{
    class GameLogic
    {
        private GameBoard Board;
        private Player Player1;
        private Player Player2;

        private int NumWinPieces; // Number of pieces in a row to win
        private PlayerTurn CurrentTurn; // Describes which player's turn it is

        public GameLogic(GameBoard board)
        {
            Board = board;
            NumWinPieces = Board.GetBoardSize();
        }

        public GameBoard GetGameBoard()
        {
            return Board;
        }

        public void StartGame()
        {
            this.CurrentTurn = PlayerTurn.PlayerOne;   // Player1 begins
            Player1.RequestMove();  // Tell Player that it's his time to play!
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
                this.Player2 = type == PlayerType.HUMAN ? (Player)new HumanPlayer(this) : (Player)new BotPlayer(this);
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
            if (Board.GetField(column, row) == FieldState.EMPTY)
            {
                Board.SetField(Field, column, row);
                Board.Refresh();

                WinState state = GetWinState();
                if (state == WinState.DRAW)
                {
                    MessageBox.Show("Draw!");
                    Application.Exit();
                }
                else if (state == WinState.XWIN)
                {
                    MessageBox.Show("X WINS");
                    Application.Exit();
                }
                else if (state == WinState.OWIN)
                {
                    MessageBox.Show("O WINS");
                    Application.Exit();
                }
                else
                {
                    FinishedMove();
                }
            }
        }

        public void FinishedMove()
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

        public WinState GetWinState()
        {
            FieldState LastPiece = FieldState.EMPTY;
            int EqualPiecesInRow = 0;

            // Check columns
            for(int column = 0; column < Board.GetBoardSize(); column++)
            {
                for(int row = 0; row < Board.GetBoardSize(); row++)
                {
                    if (LastPiece != Board.BoardState[column, row]) EqualPiecesInRow = 0;
                    LastPiece = Board.BoardState[column, row];
                    EqualPiecesInRow++;
                    if(EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                    {
                        return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                    }
                }
                LastPiece = FieldState.EMPTY;
                EqualPiecesInRow = 0;
            }

            LastPiece = FieldState.EMPTY;
            EqualPiecesInRow = 0;

            // Check rows
            for (int row = 0; row < Board.GetBoardSize(); row++)
            {
                for (int column = 0; column < Board.GetBoardSize(); column++)
                {
                    if (LastPiece != Board.BoardState[column, row]) EqualPiecesInRow = 0;
                    LastPiece = Board.BoardState[column, row];
                    EqualPiecesInRow++;
                    if (EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                    {
                        return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                    }
                }
                LastPiece = FieldState.EMPTY;
                EqualPiecesInRow = 0;
            }

            LastPiece = FieldState.EMPTY;
            EqualPiecesInRow = 0;

            // Check diagonals
            for (int row = NumWinPieces-1; row < Board.GetBoardSize(); row++)
            {
                for(int column = 0; column <= row; column++)
                {
                    if (LastPiece != Board.BoardState[column, row-column]) EqualPiecesInRow = 0;
                    LastPiece = Board.BoardState[column, row-column];
                    EqualPiecesInRow++;
                    if (EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                    {
                        return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                    }
                }
                if (row > 0)
                {
                    LastPiece = FieldState.EMPTY;
                    EqualPiecesInRow = 0;
                    for (int column = Board.GetBoardSize() - 1; column >= row; column--)
                    {
                        if (LastPiece != Board.BoardState[column, row + (Board.GetBoardSize() - 1 - column)]) EqualPiecesInRow = 0;
                        LastPiece = Board.BoardState[column, row + (Board.GetBoardSize() - 1 - column)];
                        EqualPiecesInRow++;
                        if (EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                        {
                            return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                        }
                    }
                }
                LastPiece = FieldState.EMPTY;
                EqualPiecesInRow = 0;
            }

            // Check anti diagonals
            for (int row = Board.GetBoardSize() - 1 - (NumWinPieces - 1); row >= 0; row--)
            {
                for (int column = 0; column <= Board.GetBoardSize() - 1 - row; column++)
                {
                    if (LastPiece != Board.BoardState[column, row + column]) EqualPiecesInRow = 0;
                    LastPiece = Board.BoardState[column, row + column];
                    EqualPiecesInRow++;
                    if (EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                    {
                        return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                    }
                }
                if (row > 0)
                {
                    LastPiece = FieldState.EMPTY;
                    EqualPiecesInRow = 0;
                    for (int column = Board.GetBoardSize() - 1; column >= row; column--)
                    {
                        if (LastPiece != Board.BoardState[column, row + (Board.GetBoardSize() - 1 - column)]) EqualPiecesInRow = 0;
                        LastPiece = Board.BoardState[column, row + (Board.GetBoardSize() - 1 - column)];
                        EqualPiecesInRow++;
                        if (EqualPiecesInRow == NumWinPieces && LastPiece != FieldState.EMPTY)
                        {
                            return LastPiece == FieldState.X ? WinState.XWIN : WinState.OWIN;
                        }
                    }
                }
                LastPiece = FieldState.EMPTY;
                EqualPiecesInRow = 0;
            }

            return Board.IsFull() ? WinState.DRAW : WinState.INGAME;
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

    [Flags]
    public enum WinState
    {
        INGAME = -1,
        XWIN = 0,
        OWIN = 1,
        DRAW = 2
    }

}