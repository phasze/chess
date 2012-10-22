using System;
using UvsChess;


namespace StudentAI
{
    /// <summary>
    /// This Class is simply to validate moves made.
    /// Each function should consist of these rules
    /// 1) new move is valid (conforms to piece rules)
    /// 2) 
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

        /// <summary>
        /// Checks if the white knight move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool WhiteKnight(ChessBoard board, ChessMove move)
        {
            //if not killing or empty space throw error
            if (!KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.White))
                return false;

            //if not in a style of knight movement throw error
            if(Math.Abs(move.To.X-move.From.X)==2)
            {
                if (Math.Abs(move.To.Y - move.From.Y) != 1)
                    return false;
            }
            else if (Math.Abs(move.To.X - move.From.X) == 1)
            {
                if (Math.Abs(move.To.Y - move.From.Y) != 2)
                    return false;
            }
            else
            {
                return false;
            }


            //all good
            return true;
        }

        /// <summary>
        /// Checks if the white knight move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool BlackKnight(ChessBoard board, ChessMove move)
        {
            //if not killing or empty space throw error
            if (!KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //if not in a style of knight movement throw error
            if (Math.Abs(move.To.X - move.From.X) == 2)
            {
                if (Math.Abs(move.To.Y - move.From.Y) != 1)
                    return false;
            }
            else if (Math.Abs(move.To.X - move.From.X) == 1)
            {
                if (Math.Abs(move.To.Y - move.From.Y) != 2)
                    return false;
            }
            else
            {
                return false;
            }

            //all good
            return true;
        }

        /// <summary>
        /// Checks if white rook move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool WhiteRook(ChessBoard board, ChessMove move)
        {
            //if he tries to move diagonal throw an error
            if(move.To.X!=move.From.X && move.To.Y!=move.From.Y)
            {
                return false;
            }

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.White))
                return false;

            //all good
            return true;
        }

        /// <summary>
        /// Checks if black rook move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool BlackRook(ChessBoard board, ChessMove move)
        {
            //if he tries to move diagonal throw an error
            if (move.To.X != move.From.X && move.To.Y != move.From.Y)
            {
                return false;
            }

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //all good
            return true;
        }

        /// <summary>
        /// Checks if black bishop move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool BlackBishop(ChessBoard board, ChessMove move)
        {
            //if he tries to move diagonal throw an error
            if (Math.Abs(move.From.X - move.To.X) != Math.Abs(move.From.Y - move.To.Y))
            {
                return false;
            }

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //all good
            return true;
        }

        /// <summary>
        /// Checks if white bishop move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool WhiteBishop(ChessBoard board, ChessMove move)
        {
            //if he tries to move diagonal throw an error
            if (Math.Abs(move.From.X - move.To.X) != Math.Abs(move.From.Y - move.To.Y))
            {
                return false;
            }

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.White))
                return false;

            //all good
            return true;
        }

        /// <summary>
        /// Checks if white queen move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool WhiteQueen(ChessBoard board, ChessMove move)
        {

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.White))
                return false;

            //all good
            return true;
        }

        /// <summary>
        /// Checks if white queen move is valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool BlackQueen(ChessBoard board, ChessMove move)
        {

            //if the path is not clear or they are not moving to kill then no good
            if (!PathClear(move.From.X, move.From.Y, move.To.X, move.To.Y, board) || !KillOrEmpty(move.To.X, move.To.Y, board, ChessColor.Black))
                return false;

            //all good
            return true;
        }

        //easy enough to just use inline check for this instead of calling a function
        /*private static bool CheckEmpty(int x, int y, ChessBoard board)
        {
            if (board[x, y] == ChessPiece.Empty)
                return true;
            return false;
        }*/

        //this will not check last position for enemy or blank
        private static bool PathClear(int fromX, int fromY, int toX, int toY, ChessBoard board)
        {
            //if horizontal
            if(fromY==toY)
            {
                if (toX - fromX > 0)
                    fromX++;
                else
                    fromX--;
                while (fromX != toX)
                {
                    if (board[fromX, fromY] != ChessPiece.Empty)
                        return false;
                    if (toX - fromX > 0)
                        fromX++;
                    else
                        fromX--;
                }
                return true;
            }
            //if vertical
            else if(fromX==toX)
            {
                if (toY - fromY > 0)
                    fromY++;
                else
                    fromY--;
                while (fromY != toY)
                {
                    if (board[fromX, fromY] != ChessPiece.Empty)
                        return false;
                    if (toY - fromY > 0)
                        fromY++;
                    else
                        fromY--;
                }
                return true;
            }
            //if diagonal
            /*bool Xbloodybug = false;
            bool Ybloodybug = false;
            bool XTbloodybug = false;
            bool YTbloodybug = false;

            if (fromX == 0)
            {
                fromX++;
                Xbloodybug = true;
            }
            if(fromY == 0)
            {
                fromY++;
                Ybloodybug = true;
            }
            if (toY == 0)
            {
                toY++;
                YTbloodybug = true;
            }
            if (toX == 0)
            {
                toX++;
                XTbloodybug = true;
            }*/

            if (Math.Abs(fromX - toX) == Math.Abs(fromY - toY))
            {
                /*if (Xbloodybug)
                    fromX--;
                if (Ybloodybug)
                    fromY--;
                if (XTbloodybug)
                    toX--;
                if (YTbloodybug)
                    toY--;*/

                //don't check the same location
                if (toY - fromY > 0)
                    fromY++;
                else
                    fromY--;

                if (toX - fromX > 0)
                    fromX++;
                else
                    fromX--;

                while (fromY != toY)
                {
                    if (board[fromX, fromY] != ChessPiece.Empty)
                        return false;
                    if (toY - fromY > 0)
                        fromY++;
                    else
                        fromY--;

                    if (toX - fromX > 0)
                        fromX++;
                    else
                        fromX--;
                }
                return true;
            }
            //if none of the above
            return false;
        }

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
            if (color == ChessColor.Black)
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
