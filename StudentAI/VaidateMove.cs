using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UvsChess;


namespace StudentAI
{
    /// <summary>
    /// This Class is simply to validate moves made.
    /// Each function should consist of these rules
    /// 1) new move is valid (still on board and conforms to piece rules)
    /// 2) ensure it doesn't put their own king into check
    /// 3) Doesn't kill it's own team
    /// </summary>
    class VaidateMove
    {
        /// <summary>
        /// Checks if the move the white king wants to make is valid
        /// </summary>
        /// <param name="board">board state before move</param>
        /// <param name="move">move the piece wants to make</param>
        /// <returns>true if valid move, false is invalid</returns>
        public static bool WhiteKing(ChessBoard board, ChessMove move)
        {
            //TODO NOT DONE, still need to finish this
            //validate the move is still on the board as well as
            //  ensure new move is onto an enemy piece or empty space
            if (!OnBoardAndKillOrEmpty(move.To.X, move.To.Y,board,ChessColor.White))
                return false;

            //validate it doesn't put self into check
            //VS2012 test


            //if all checks pass then
            return true;
        }

        private static bool checkEmpty(int X, int Y, ChessBoard board)
        {
            if (board[X, Y] == ChessPiece.Empty)
                return true;
            return false;
        }


        //checks if the move is still located on the board & is moved onto empty or kill spot
        private static bool OnBoardAndKillOrEmpty(int X, int Y,ChessBoard board,ChessColor color)
        {
            if (X > 8 || X < 0 || Y > 8 || Y < 0)
                return false;

            if(board[X,Y] == ChessPiece.Empty)
            {
                if(color==ChessColor.White)
                {
                    switch (board[X,Y])
                    {
                        case ChessPiece.BlackPawn:
                        case ChessPiece.BlackKnight:
                        case ChessPiece.BlackBishop:
                        case ChessPiece.BlackQueen:
                        case ChessPiece.BlackRook:
                        case ChessPiece.BlackKing:
                            return true;
                        default:
                            return false;
                    }
                }
                else if(color==ChessColor.Black)
                {
                    switch (board[X, Y])
                    {
                        case ChessPiece.WhiteBishop:
                        case ChessPiece.WhiteKing:
                        case ChessPiece.WhiteKnight:
                        case ChessPiece.WhitePawn:
                        case ChessPiece.WhiteQueen:
                        case ChessPiece.WhiteRook:
                            return true;
                        default:
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
