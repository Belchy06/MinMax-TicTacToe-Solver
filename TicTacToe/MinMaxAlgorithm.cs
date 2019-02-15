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
        //private List<AIMove> AIMoves = new List<AIMove>();

        public struct AIMove 
        {
            public AIMove(int? X, int? Y, int? Score)
            {
                x = X;
                y = Y;
                score = Score;
            }
            public int? x;
            public int? y;
            public int? score;
        }

        public MinMaxAlgorithm(GameLogic logic, Player player)
        {
            this.Logic = logic;
            this.BotPlayer = player;
        }

        public void PerformMove()
        {
            AIMove bestMove = CalculateBestMove(BotPlayer, -100000, 100000);
            Logic.MakeMove((int)bestMove.x, (int)bestMove.y, BotPlayer);
        }

        public AIMove CalculateBestMove(Player player, long alpha, long beta)
        {
            WinState W = Logic.GetWinState();

            switch(W)
            {
                case WinState.XWIN:
                    return new AIMove(null, null, BotPlayer.GetRole() == FieldState.X ? 10 : -10); // Win for X

                case WinState.OWIN:
                    return new AIMove(null, null, BotPlayer.GetRole() == FieldState.O ? 10 : -10); // Win for O

                case WinState.DRAW:
                    return new AIMove(null, null, 0); // Draw

                default:
                    break;
            }

            List<AIMove> AIMoves = new List<AIMove>();

            for (int y = 0; y < Logic.GetGameBoard().GetBoardSize(); y++)
            {
                for (int x = 0; x < Logic.GetGameBoard().GetBoardSize(); x++) // Iterates through the board to find empty cells. 
                                                                              // This is where I would have used the list of empty cells
                {
                    if (Logic.GetGameBoard().BoardState[x, y] == FieldState.EMPTY)
                    {
                        AIMove move; // Initialize struct
                        move.x = x;     // Store x value of current empty cell
                        move.y = y;     // Store y value of current empty cell
                        Logic.GetGameBoard().SetField(player.GetRole(), x, y);

                        // Recursivly call method with the opponent of the current player 
                        move.score = CalculateBestMove(player == Logic.GetPlayer1() ? Logic.GetPlayer2() : Logic.GetPlayer1(), alpha, beta).score;
                        if (Logic.GetPlayer1().GetPlayerType() == PlayerType.ROBOT)
                        {
                            alpha = Math.Max(alpha, (long)move.score);
                            if (beta <= alpha) break;
                        }
                        else
                        {
                            beta = Math.Min(beta, (long)move.score);
                            if (beta <= alpha) break;
                        }

                        AIMoves.Add(move);
                        Logic.GetGameBoard().SetField(FieldState.EMPTY, x, y); // Set the board cell back to empty after testing move
                    }
                }
            }

            int BestMove = 0;
            if (player.GetPlayerType() == PlayerType.ROBOT)
            {
                int BestScore = -10000;
                for (int i = 0; i < AIMoves.Count(); i++)
                {
                    if (AIMoves[i].score > BestScore)
                    {
                        BestMove = i;
                        BestScore = (int)AIMoves[i].score;
                    }
                }
            }
            else
            {
                int BestScore = 10000;
                for (int i = 0; i < AIMoves.Count(); i++)
                {
                    if (AIMoves[i].score < BestScore)
                    {
                        BestMove = i;
                        BestScore = (int)AIMoves[i].score;
                    }
                }
            }

            return AIMoves[BestMove];
        }
    }
}