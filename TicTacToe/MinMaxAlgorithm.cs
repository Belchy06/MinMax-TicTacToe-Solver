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
        private Player BotPlayer;
        

        // Should move this to a class but it's okay as a struct for now
        // This structure is used to store the variables of a move. Once the optimal move is found, the X and Y variables will be used in Logic.GetGameBoard().SetField
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

        public MinMaxAlgorithm(GameLogic logic, Player player)
        {
            this.Logic = logic;
            this.BotPlayer = player;
        }

        public void PerformMove()
        {
            AIMove bestMove = CalculateBestMove(BotPlayer);
            Logic.MakeMove((int)bestMove.X, (int)bestMove.Y, BotPlayer);
        }

        public AIMove CalculateBestMove(Player player)
        {
            WinState W = Logic.GetWinState();

            // Base case
            if (W == WinState.XWIN)
            {
                return new AIMove(null, null, BotPlayer.GetRole() == FieldState.X ? 10 : -10); // Win for X
            }
            else if (W == WinState.OWIN)
            {
                return new AIMove(null, null, BotPlayer.GetRole() == FieldState.O ? 10 : -10); // Win for O
            }
            else if (W == WinState.DRAW)
            {
                return new AIMove(null, null, 0); // Draw
            }

            FieldState Field = player.GetRole();

            List<AIMove> Moves = new List<AIMove>(); //Store a list of the recursive moves

            // Do recursive function calls and construct list of moves
            for (int y = 0; y < Logic.GetGameBoard().GetBoardSize(); y++)
            {
                for (int x = 0; x < Logic.GetGameBoard().GetBoardSize(); x++) // Iterates through the board to find empty cells. 
                                                                   // This is where I would have used the list of empty cells
                {
                    if (Logic.GetGameBoard().BoardState[x,y] == FieldState.EMPTY)
                    {
                        AIMove move; // Initialize struct
                        move.X = x;     // Store x value of current empty cell
                        move.Y = y;     // Store y value of current empty cell
                        Logic.GetGameBoard().SetField(Field, x, y);

                        // Recursivly call method with the opponent of the current player 
                        move.Score = CalculateBestMove(player == Logic.GetPlayer1() ? Logic.GetPlayer2() : Logic.GetPlayer1()).Score;

                        Moves.Add(move);
                        Logic.GetGameBoard().SetField(FieldState.EMPTY, x, y); // Set the board cell back to empty after testing move
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