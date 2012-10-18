using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  UvsChess;

namespace StudentAI
{
    class Minimax
    {
        public ChessMove Minimax(StudentAI AI, ChessBoard board, ChessColor color, ChessMove alpha, ChessMove beta, int depth)
        {
            if (depth <= 0)
                return alpha;
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

                if (AI. IsKingInCheck(tempBoard, oppositeColor))
                {
                    move.Flag = ChessFlag.Check;
                    //check for checkmate
                    if (PieceMoves.getmovesofcolor(AI, oppositeColor, tempBoard).Count == 0)
                        move.Flag = ChessFlag.Checkmate;
                }

                HueristicMoves.Add(new Hueristic(board, move, oppositeColor));
                if (move.Flag == ChessFlag.Check)
                    HueristicMoves[HueristicMoves.Count - 1].HValue -= 5;
                if (move.Flag == ChessFlag.Checkmate)
                    HueristicMoves[HueristicMoves.Count - 1].HValue = 0;
            }
            foreach (var hmove in HueristicMoves)
            {
                var tempBoard = board.Clone();
                tempBoard.MakeMove(hmove.TheMove);

                ChessMove oppositemove = Minimax(AI, tempBoard, oppositeColor, alpha, beta, depth - 1);
                if (new Hueristic(tempBoard, oppositemove, color).HValue > new Hueristic(tempBoard,alpha,color).HValue)
                {
                    alpha = oppositemove;
                }
            }
            return alpha;
        }
        // for all moves call minimax with depth -1 with that move to other color
    }
}
