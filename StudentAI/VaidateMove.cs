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
            //validate the move is still on the board as well as
            //  ensure new move is onto an enemy piece or empty space
            if (!OnBoardAndKillOrEmpty(move.To.X, move.To.Y,board,ChessColor.White))
                return false;

            //validate the move is only one space away for a king
            if (Math.Abs(move.To.X - move.From.X) > 1 || Math.Abs(move.To.Y - move.From.Y) > 1)
                return false;

            //TODO validate it doesn't put self into check


            //if all checks pass then
            return true;
        }


        /// <summary>
        /// Checks if the move the black king wants to make is valid
        /// </summary>
        /// <param name="board">board state before move</param>
        /// <param name="move">move the piece wants to make</param>
        /// <returns>true if valid move, false is invalid</returns>
        public static bool BlackKing(ChessBoard board, ChessMove move)
        {
            //validate the move is still on the board as well as
            //  ensure new move is onto an enemy piece or empty space
            if (!OnBoardAndKillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //validate the move is only one space away for a king
            if (Math.Abs(move.To.X - move.From.X) > 1 || Math.Abs(move.To.Y - move.From.Y) > 1)
                return false;

            //TODO validate it doesn't put self into check


            //if all checks pass then
            return true;
        }

        //easy enough to just use inline check for this instead of calling a function
        /*private static bool CheckEmpty(int x, int y, ChessBoard board)
        {
            if (board[x, y] == ChessPiece.Empty)
                return true;
            return false;
        }*/


        //checks if the move is still located on the board & is moved onto empty or kill spot
        private static bool OnBoardAndKillOrEmpty(int x, int y,ChessBoard board,ChessColor color)
        {
            if (x > 8 || x < 0 || y > 8 || y < 0)
                return false;

            if(board[x,y] == ChessPiece.Empty)
            {
                if(color==ChessColor.White)
                {
                    switch (board[x,y])
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
                if(color==ChessColor.Black)
                {
                    switch (board[x, y])
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
        private static bool indanger(int x, int y, ChessBoard board, ChessColor color)
        {
            if (color == ChessColor.White)
            {
                if (board[x - 1, y - 1] == ChessPiece.BlackPawn || board[x + 1, y - 1] == ChessPiece.BlackPawn)
                    return true;


            }
            if (color == ChessColor.Black)
            {




            }

        }
    }
}
