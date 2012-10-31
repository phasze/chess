using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using  UvsChess;

namespace StudentAI
{
    class Minimax
    {
        static Random rand = new Random();
        public static Hueristic _bestMove = null;
        //private static ChessColor myColor;
        public static bool timerUp = false;
        public int maxdepth = 1;

        public static void getMoveThread()//(StudentAI AI, ChessBoard board, ChessColor color, int depth)
        {
            //myColor = color;
            Thread.Sleep(4000);
            timerUp = true;
            //getMinimax(AI, board, color, depth);
        }

        public Hueristic getMinimax(StudentAI AI, ChessBoard board, ChessColor color, int depth, ChessColor maxColor, int alpha = -999999, int beta = 999999)
        {
            //TODO update to return heuristic instead of move and update get next move to return move of heuristic.
            if (depth > maxdepth)
            {
                return new Hueristic(board,new ChessMove(null, null),color);
            }
            List<ChessMove> allmoves = new List<ChessMove>();
            allmoves.AddRange(PieceMoves.getmovesofcolor(AI, color, board));
            List<Hueristic> HueristicMoves = new List<Hueristic>();
            //DecisionTree descisions = new DecisionTree(board);
            //descisions.

            ChessColor oppositeColor = color == ChessColor.Black ? ChessColor.White : ChessColor.Black;

            //set check/checkmate flag and calculate hueristic on each move
            foreach (var move in allmoves)
            {
                var tempBoard = board.Clone();
                tempBoard.MakeMove(move);

                if (StudentAI.IsKingInCheck(tempBoard, oppositeColor))
                {
                    move.Flag = ChessFlag.Check;
                    //check for checkmate
                    if (PieceMoves.getmovesofcolor(AI, oppositeColor, tempBoard).Count == 0)
                    {
                        move.Flag = ChessFlag.Checkmate;
                        return new Hueristic(board,move,color);
                    }
                }

                HueristicMoves.Add(new Hueristic(board, move, color));
                if (color == maxColor && move.Flag == ChessFlag.Check)
                    HueristicMoves[HueristicMoves.Count - 1].HValue += 2;
                if (color == maxColor && move.Flag == ChessFlag.Checkmate)
                {
                    HueristicMoves[HueristicMoves.Count - 1].HValue = 10000;
                    return HueristicMoves[HueristicMoves.Count -1];
                }
            }
            
            HueristicMoves.Sort((x, y) => y.HValue.CompareTo(x.HValue));


            if (AI.IsMyTurnOver() && HueristicMoves.Count > 0)
                return HueristicMoves[0];

            if (depth == maxdepth && HueristicMoves.Count>0)
                return HueristicMoves[0];
            List<Hueristic> updatedhueristic = new List<Hueristic>();
            //minimax and alpha beta pruning
            
            while (!AI.IsMyTurnOver())
            {
                updatedhueristic = new List<Hueristic>();
                foreach (var hmove in HueristicMoves)
                {
                    //TODO if player = maxplayer store a to be max then if beta <= alpha break
                    
                    var tempBoard = board.Clone();
                    if (hmove.TheMove != null)
                    {
                        tempBoard.MakeMove(hmove.TheMove);
                        if (depth != maxdepth)
                        {
                            var oppositemove = getMinimax(AI, tempBoard, oppositeColor, depth + 1, maxColor, alpha, beta); //get best move of the other color

                            if (oppositemove.TheMove.To != null && oppositemove.TheMove.From != null)
                            {
                                hmove.HValue -= oppositemove.HValue; // update our moves score based on return of projected other move
                            }
                        }
                        updatedhueristic.Add(hmove); // add new scored hueristic to new list
                        if (AI.IsMyTurnOver())
                            break;
                        //a=max(a,hueristic)
                        if (maxColor == color)
                        {
                            alpha = alpha > hmove.HValue ? alpha : hmove.HValue;
                            if (beta <= alpha)
                                break;
                        }
                        else
                        {
                            beta = beta < hmove.HValue ? beta : hmove.HValue;
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                if (!AI.IsMyTurnOver())
                {
                    if (depth == maxdepth)
                    {
                        maxdepth += 1;
                    }
                }
            }

            updatedhueristic.Sort((x, y) => y.HValue.CompareTo(x.HValue)); // sort the new list

            if (color == maxColor)
            {
                if (updatedhueristic.Count == 0)
                {
                    var game_over = new ChessMove(null, null);
                    game_over.Flag = ChessFlag.Stalemate;
                    var game_overhueristic = new Hueristic(board, game_over, color);
                    return game_overhueristic;

                }
                int tiecount = -1;
                foreach (var x in updatedhueristic)
                    if (x.HValue == updatedhueristic[0].HValue)
                        tiecount++;
                if (tiecount > 0)
                    return updatedhueristic[rand.Next(0, tiecount)];
            }

            if (updatedhueristic.Count == 0)
                return new Hueristic(board,new ChessMove(null, null),color);
            return updatedhueristic[0];      //return the best value from the new list
        }
    }
}
