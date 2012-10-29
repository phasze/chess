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

        public static void getMoveThread()//(StudentAI AI, ChessBoard board, ChessColor color, int depth)
        {
            //myColor = color;
            Thread.Sleep(4500);
            timerUp = true;
            //getMinimax(AI, board, color, depth);
        }

        public ChessMove getMinimax(StudentAI AI, ChessBoard board, ChessColor color, int depth, ChessColor maxColor, int alpha = -999999, int beta = 999999)
        {
            if (depth < 0)
            {
                return new ChessMove(null, null);
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
                        return move;
                    }
                }

                HueristicMoves.Add(new Hueristic(board, move, color));
                if (color == maxColor && move.Flag == ChessFlag.Check)
                    HueristicMoves[HueristicMoves.Count - 1].HValue += 7;
                if (color == maxColor && move.Flag == ChessFlag.Checkmate)
                {
                    HueristicMoves[HueristicMoves.Count - 1].HValue = 10000;
                    return move;
                }
            }
            List<Hueristic> updatedhueristic = new List<Hueristic>();
            HueristicMoves.Sort((x, y) => y.HValue.CompareTo(x.HValue));


            if (AI.IsMyTurnOver() && HueristicMoves.Count > 0)
                return HueristicMoves[0].TheMove;

            if (depth == 0 && HueristicMoves.Count>0)
                return HueristicMoves[0].TheMove;

            //minimax and alpha beta pruning
            foreach (var hmove in HueristicMoves)
            {
                //TODO if player = maxplayer store a to be max then if beta <= alpha break

                var tempBoard = board.Clone();
                if (hmove.TheMove!= null)
                {
                    tempBoard.MakeMove(hmove.TheMove);

                    ChessMove oppositemove = getMinimax(AI, tempBoard, oppositeColor, depth - 1,maxColor,alpha,beta); //get best move of the other color

                    if (oppositemove.To != null && oppositemove.From != null)
                    {
                        var oppositemovehueristic = new Hueristic(tempBoard, oppositemove, oppositeColor); // calculate the score of the board
                        hmove.HValue -= oppositemovehueristic.HValue; // update our moves score based on return of projected other move
                    }
                    updatedhueristic.Add(hmove); // add new scored hueristic to new list
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

            updatedhueristic.Sort((x, y) => y.HValue.CompareTo(x.HValue)); // sort the new list

            if (color == maxColor)
            {
                if (updatedhueristic.Count == 0)
                {
                    var game_over = new ChessMove(null, null);
                    game_over.Flag = ChessFlag.Stalemate;
                    return game_over;

                }
                int tiecount = -1;
                foreach (var x in updatedhueristic)
                    if (x.HValue == updatedhueristic[0].HValue)
                        tiecount++;
                if (tiecount > 0)
                    return updatedhueristic[rand.Next(0, tiecount)].TheMove;
            }

            if (updatedhueristic.Count == 0)
                return new ChessMove(null, null);
            return updatedhueristic[0].TheMove;      //return the best value from the new list
        }
    }
}
