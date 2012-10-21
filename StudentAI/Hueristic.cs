﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  UvsChess;

namespace StudentAI
{
    class Hueristic
    {
        public ChessBoard BoardAfterMove = null;
        public ChessBoard BoardBeforeMove = null;
        public ChessMove TheMove=null;
        public int HValue = 0;

        /// <summary>
        /// this will define BoardAfterMove, TheMove, and HValue based on move
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <param name="colorofEnemy"></param>
        public Hueristic(ChessBoard board, ChessMove move, ChessColor colorofEnemy)
        {
            BoardBeforeMove = board.Clone();
            //BoardAfterMove.MakeMove(move);
            TheMove = move;
            //HValue = CalculateHueristicBasic(board, colorofEnemy);
            HValue = CalculateHueristicAdvanced(colorofEnemy);
        }

        /// <summary>
        /// This will define BoardBeforeMove, and HValue based on your pieces, higher being better
        /// </summary>
        /// <param name="board"></param>
        /// <param name="colorofMyTeam"></param>
        public Hueristic(ChessBoard board, ChessColor colorofMyTeam)
        {
            BoardBeforeMove = board.Clone();
            //BoardAfterMove = board.Clone();
            HValue = CalculateHueristicAdvanced(colorofMyTeam);
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
                    if (colorOfEnemyTeam == ChessColor.Black && BoardAfterMove[x, y] < ChessPiece.Empty) //if black
                        score += StudentAI.Piece.CalculatePieceValue(BoardAfterMove[x, y]);
                    else if (colorOfEnemyTeam == ChessColor.White && BoardAfterMove[x, y] > ChessPiece.Empty) //if white
                        score += StudentAI.Piece.CalculatePieceValue(BoardAfterMove[x, y]);
                }
            }

            return score - StudentAI.Piece.CalculatePieceActionValue(board[TheMove.From.X, TheMove.From.Y]);

        }


        /// <summary>
        /// The lower the number returned the better off you are
        /// </summary>
        /// <param name="board"></param>
        /// <param name="colorOfEnemyTeam"></param>
        /// <returns></returns>
        int CalculateHueristicAdvanced(ChessColor colorOfEnemyTeam)
        {
            int scoreB = 0;
            int scoreW = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (BoardBeforeMove[x, y] < ChessPiece.Empty) //if black
                        scoreB += StudentAI.Piece.CalculatePieceValue(BoardBeforeMove[x, y]);
                    else if (BoardBeforeMove[x, y] > ChessPiece.Empty) //if white
                        scoreW += StudentAI.Piece.CalculatePieceValue(BoardBeforeMove[x, y]);
                }
            }

            return colorOfEnemyTeam == ChessColor.Black ? scoreB - scoreW + StudentAI.Piece.CalculatePieceActionValue(BoardBeforeMove[TheMove.From.X, TheMove.From.Y]) 
                : scoreW - scoreB + StudentAI.Piece.CalculatePieceActionValue(BoardBeforeMove[TheMove.From.X, TheMove.From.Y]);

            //return score - StudentAI.Piece.CalculatePieceActionValue(board[TheMove.From.X, TheMove.From.Y]);

        }

        /// <summary>
        /// High Number = better board for your color
        /// </summary>
        /// <param name="board"></param>
        /// <param name="colorOfEnemyTeam"></param>
        /// <returns></returns>
        int CalculateBoardHP(ChessBoard board, ChessColor colorOfMyTeam)
        {
            int score = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (colorOfMyTeam == ChessColor.Black && BoardAfterMove[x, y] < ChessPiece.Empty) //if black
                        score += StudentAI.Piece.CalculatePieceValue(BoardAfterMove[x, y]);
                    else if (colorOfMyTeam == ChessColor.White && BoardAfterMove[x, y] > ChessPiece.Empty) //if white
                        score += StudentAI.Piece.CalculatePieceValue(BoardAfterMove[x, y]);
                }
            }

            return score;

        }
    }
}
