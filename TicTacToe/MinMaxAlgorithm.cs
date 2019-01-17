using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TicTacToe
{
    class MinMaxAlgorithm
    {
        private GameLogic Logic;
        private GameBoard Board;
        private Player BotPlayer;
        

        // Should move this to a class but it's okay as a struct for now
        // This structure is used to store the variables of a move. Once the optimal move is found, the X and Y variables will be used in Board.SetField
        // Use of nullable as struct constructors must contain all variables, so for when we don't want to give X and value, we give it Null
        public struct AIMove 
        {
            public AIMove(Nullable<int> x, Nullable<int> y, int score)
            {
                X = x;
                Y = y;
                Score = score;
            }
            public Nullable<int> X;
            public Nullable<int> Y;
            public Nullable<int> Score;
        }

        public MinMaxAlgorithm(GameLogic logic, Player player, GameBoard board)
        {
            this.Logic = logic;
            this.BotPlayer = player;
            this.Board = board;
        }

        public void PerformMove(GameBoard board)
        {
            AIMove bestMove = CalculateBestMove(board, Logic.GetPlayer2());
            Logic.MakeMove((int)bestMove.X, (int)bestMove.Y, Logic.GetPlayer2());
        }

        public AIMove CalculateBestMove(GameBoard board, Player player)
        {
            this.Board = board;

            // Base case
            if (Logic.CheckWinState(Board, FieldState.X) == EndState.XWin)
            {
                return new AIMove(null, null, -10); // Player wins
            } else if (Logic.CheckWinState(Board, FieldState.O) == EndState.OWin)
            {
                return new AIMove(null, null, 10); // AI wins
            } else if(Logic.CheckWinState(Board, FieldState.X) == EndState.Draw || Logic.CheckWinState(Board, FieldState.O) == EndState.Draw)
            {
                return new AIMove(null, null, 0); // Draw
            }

            List<AIMove> Moves = new List<AIMove>(); //Store a list of the recursive moves

            FieldState Field = player.GetRole();

            // Do recursive function calls and construct list of moves
            for (int y = 0; y < GameBoard.getBoardSize(); y++)
            {
                for (int x = 0; x < GameBoard.getBoardSize(); x++) // Iterates through the board to find empty cells. 
                                                                   // This is where I would have used the list of empty cells
                {
                    if (board.BoardState[x,y] == FieldState.EMPTY)
                    {
                        AIMove move; // Initialize struct
                        move.X = x;     // Store x value of current empty cell
                        move.Y = y;     // Store y value of current empty cell
                        board.SetField(Field, x, y);

                        if(player.GetPlayerType() == PlayerType.ROBOT) 
                        {
                            move.Score = CalculateBestMove(Board, Logic.GetPlayer1()).Score; // If the Bot called this function then call the function again but for the player
                        }
                        else
                        {
                            move.Score = CalculateBestMove(Board, Logic.GetPlayer2()).Score; // Else the player called the function and so now recall for the robot
                        }
                        
                        Moves.Add(move);
                        board.SetField(FieldState.EMPTY, x, y); // Set the board cell back to empty after testing move
                    }
                }
            }


            // Pick the best move for the current player
            int BestMove = 0;
            if(player.GetPlayerType() == PlayerType.ROBOT)
            {
                int BestScore = -10000;
                for(int i = 0; i < Moves.Count(); i++)
                {
                    if(Moves[i].Score > BestScore)
                    {
                        BestMove = i;
                        BestScore = (int)Moves[i].Score;
                    }
                }
            }
            else
            {
                int BestScore = 10000;
                for (int i = 0; i < Moves.Count(); i++)
                {
                    if (Moves[i].Score < BestScore)
                    {
                        BestMove = i;
                        BestScore = (int)Moves[i].Score;
                    }
                }
            }

            // Return best move
            return Moves[BestMove];
        }
    }
}