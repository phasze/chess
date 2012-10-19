using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  UvsChess;

namespace StudentAI
{
    class Minimax
    {
        Random rand = new Random();
        public ChessMove getMinimax(StudentAI AI, ChessBoard board, ChessColor color, int depth)
        {
            if (depth <= 0)
            {
                return new ChessMove(null, null);
            }
            List<ChessMove> allmoves = new List<ChessMove>();
            allmoves.AddRange(PieceMoves.getmovesofcolor(AI, color, board));
            List<Hueristic> HueristicMoves = new List<Hueristic>();
            //DecisionTree descisions = new DecisionTree(board);
            //descisions.

            ChessColor oppositeColor = color == ChessColor.Black ? ChessColor.White : ChessColor.Black;

            //TODO possibly temorary, but set the check flag
            foreach (var move in allmoves)
            {
                var tempBoard = board.Clone();
                tempBoard.MakeMove(move);

                if (StudentAI.IsKingInCheck(tempBoard, oppositeColor))
                {
                    move.Flag = ChessFlag.Check;
                    //check for checkmate
                    if (PieceMoves.getmovesofcolor(AI, oppositeColor, tempBoard).Count == 0)
                        move.Flag = ChessFlag.Checkmate;
                }

                HueristicMoves.Add(new Hueristic(board, move, color));
                if (move.Flag == ChessFlag.Check)
                    HueristicMoves[HueristicMoves.Count - 1].HValue -= 5;
                if (move.Flag == ChessFlag.Checkmate)
                    HueristicMoves[HueristicMoves.Count - 1].HValue = 0;
            }
            List<Hueristic> updatedhueristic = new List<Hueristic>();
            HueristicMoves.Sort((x, y) => x.HValue.CompareTo(y.HValue));
            foreach (var hmove in HueristicMoves)
            {
                var tempBoard = board.Clone();
                tempBoard.MakeMove(hmove.TheMove);

                ChessMove oppositemove = getMinimax(AI, tempBoard, oppositeColor, depth - 1); //get best move of the other color
                tempBoard.MakeMove(oppositemove); //update the board with the new move
                var oppositemovehueristic = new Hueristic(tempBoard, color); // calculate the score of the board
                hmove.HValue -= oppositemovehueristic.HValue; // update our moves score based on return of projected other move
                updatedhueristic.Add(hmove); // add new scored hueristic to new list
            }
            updatedhueristic.Sort((x, y) => x.HValue.CompareTo(y.HValue)); // sort the new list
            int tiecount = -1;
            foreach (var x in updatedhueristic)
                if (x.HValue == updatedhueristic[0].HValue)
                    tiecount++;
            if (tiecount > 0)
                return updatedhueristic[rand.Next(0, tiecount)].TheMove;
            else
                return updatedhueristic[0].TheMove;      //return the best value from the new list
        }
        // for all moves call minimax with depth -1 with that move to other color
    }
}
