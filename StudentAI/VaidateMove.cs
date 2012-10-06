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
            //  ensure new move is onto an enemy piece or empty space
            if (!KillOrEmpty(move.To.X, move.To.Y,board,ChessColor.White))
                return false;

            //validate the move is only one space away for a king
            if (Math.Abs(move.To.X - move.From.X) > 1 || Math.Abs(move.To.Y - move.From.Y) > 1)
                return false;

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
            //  ensure new move is onto an enemy piece or empty space
            if (!KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //validate the move is only one space away for a king
            if (Math.Abs(move.To.X - move.From.X) > 1 || Math.Abs(move.To.Y - move.From.Y) > 1)
                return false;

            //if all checks pass then
            return true;
        }

        /// <summary>
        /// Checks if the move the white pawn wants to make is valid
        /// </summary>
        /// <param name="board">board state before move</param>
        /// <param name="move">move the piece wants to make</param>
        /// <returns>true if valid move, false is invalid</returns>
        public static bool WhitePawn(ChessBoard board, ChessMove move)
        {
            //if pawn tries to move backwords
            if (move.To.Y >= move.From.Y) //white pawns Y goes up or less than
                return false;

            //validate move is still on board & is empty or kill
            if(move.From.Y==6 && move.To.Y==4) //trying to move two places from start
            {
                //make sure they're moving to the same column
                if (move.To.X != move.From.X)
                    return false;

                //if both spaces are not empty
                if (board[move.To.X, move.To.Y] != ChessPiece.Empty || board[move.To.X, move.To.Y + 1] != ChessPiece.Empty) 
                    return false;
            }
            //if just moving forward by one
            else if(move.From.Y-move.To.Y==1)
            {
                //if going for kill diagnoally
                if (Math.Abs(move.From.X - move.To.X) == 1)
                {
                    if (!IsEnemy(move.To.X, move.To.Y, board, ChessColor.White))
                        return false;
                }
                //if just moving forward
                else if (move.From.X == move.To.X)
                {
                    if(board[move.To.X, move.To.Y] != ChessPiece.Empty)
                        return false;
                }
                //otherwise false if moving too far horizontally
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            //all checks pass
            return true;
        }

        /// <summary>
        /// Checks if the move the white pawn wants to make is valid
        /// </summary>
        /// <param name="board">board state before move</param>
        /// <param name="move">move the piece wants to make</param>
        /// <returns>true if valid move, false is invalid</returns>
        public static bool BlackPawn(ChessBoard board, ChessMove move)
        {
            //if pawn tries to move backwords
            if (move.To.Y <= move.From.Y) //white pawns Y goes up or less than
                return false;

            //validate move is still on board & is empty or kill
            if (move.From.Y == 1 && move.To.Y == 3) //trying to move two places from start
            {
                //make sure they're moving to the same column
                if (move.To.X != move.From.X)
                    return false;

                //if both spaces are not empty
                if (board[move.To.X, move.To.Y] != ChessPiece.Empty || board[move.To.X, move.To.Y - 1] != ChessPiece.Empty)
                    return false;
            }
            //if just moving forward by one
            else if (move.To.Y - move.From.Y == 1)
            {
                //if going for kill diagnoally
                if (Math.Abs(move.From.X - move.To.X) == 1)
                {
                    if (!IsEnemy(move.To.X, move.To.Y, board, ChessColor.Black))
                        return false;
                }
                //if just moving forward
                else if (move.From.X == move.To.X)
                {
                    if (board[move.To.X, move.To.Y] != ChessPiece.Empty)
                        return false;
                }
                //otherwise false if moving too far horizontally
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            //all checks pass
            return true;
        }



        //easy enough to just use inline check for this instead of calling a function
        /*private static bool CheckEmpty(int x, int y, ChessBoard board)
        {
            if (board[x, y] == ChessPiece.Empty)
                return true;
            return false;
        }*/


        //check if move is enemy
        private static bool IsEnemy(int x, int y, ChessBoard board, ChessColor color)
        {
            if (color == ChessColor.White)
            {
                switch (board[x, y])
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
            else if (color == ChessColor.Black)
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
            throw new Exception("No Color defined");
        }

        //checks if the move is still located on the board & is moved onto empty or kill spot
        private static bool KillOrEmpty(int x, int y,ChessBoard board,ChessColor color)
        {

            if(board[x,y] != ChessPiece.Empty)
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
    }
}
