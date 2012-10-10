using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  UvsChess;

namespace StudentAI
{
    class Hueristic
    {
        public ChessBoard BoardAfterMove = null;
        public ChessMove TheMove=null;
        public int HValue = 0;
        public Hueristic(ChessBoard board, ChessMove move, ChessColor colorofEnemy)
        {
            BoardAfterMove = board.Clone();
            BoardAfterMove.MakeMove(move);
            TheMove = move;
            HValue = CalculateHueristicBasic(BoardAfterMove, colorofEnemy);
        }

        /// <summary>
        /// The lower the number returned the better off you are
        /// </summary>
        /// <param name="board"></param>
        /// <param name="colorOfEnemyTeam"></param>
        /// <returns></returns>
        int CalculateHueristicBasic(ChessBoard board, ChessColor colorOfEnemyTeam)
        {
            int score = 0;
            for(int x=0;x<8;x++)
            {
                for(int y=0;y<8;y++)
                {
                    if (colorOfEnemyTeam == ChessColor.Black && board[x, y] < ChessPiece.Empty) //if black
                        score+=StudentAI.Piece.CalculatePieceValue(board[x,y]) + StudentAI.Piece.CalculatePieceActionValue(board[TheMove.To.X,TheMove.To.Y]);
                    else if (colorOfEnemyTeam == ChessColor.White && board[x, y] > ChessPiece.Empty) //if white
                        score += StudentAI.Piece.CalculatePieceValue(board[x, y]) + StudentAI.Piece.CalculatePieceActionValue(board[TheMove.To.X, TheMove.To.Y]);
                }
            }

            return score;

        }
    }
}
