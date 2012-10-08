using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UvsChess;

namespace StudentAI
{
    /// <summary>
    /// Methods to getmovesofcolor all moves of a piece
    /// </summary>
    class PieceMoves
    {
        /// <summary>
        /// gets a list of available moves for a color
        /// </summary>
        public List<ChessMove> getmovesofcolor(ChessColor color, ChessBoard board)
        {
            List<ChessMove> colormoves = new List<ChessMove>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (color == ChessColor.White)
                    {

                    }
                    if (color == ChessColor.Black)
                    {

                    }
                }
            }
            return colormoves;
        }

        /// <summary>
        /// gets move for a specfic piece
        /// </summary>
        public List<ChessMove> getmovesofpiece(StudentAI ai,ChessColor color, ChessBoard board,ChessLocation location)
        {
            List<ChessMove> piecemoves = new List<ChessMove>();
            ChessMove move = new ChessMove(location,location);
            switch (board[location])
            {
                case ChessPiece.BlackPawn:
                    ChessMove bdiag1 = new ChessMove(location,new ChessLocation((location.X)-1,(location.Y)+1));
                    ChessMove bdown = new ChessMove(location,new ChessLocation((location.X),(location.Y)+1));
                    ChessMove bdiag2 = new ChessMove(location,new ChessLocation((location.X)+1,(location.Y)+1));
                    if (ai.IsValidMove(board, bdiag1, color))
                        piecemoves.Add(bdiag1);
                    if (ai.IsValidMove(board, bdown, color))
                        piecemoves.Add(bdown);
                    if (ai.IsValidMove(board, bdiag2, color))
                        piecemoves.Add(bdiag2);
                    break;
                case ChessPiece.WhitePawn:
                    ChessMove wdiag1 = new ChessMove(location,new ChessLocation((location.X)-1,(location.Y)-1));
                    ChessMove wup = new ChessMove(location,new ChessLocation((location.X),(location.Y)-1));
                    ChessMove wdiag2 = new ChessMove(location,new ChessLocation((location.X)+1,(location.Y)-1));
                    if (ai.IsValidMove(board, wdiag1, color))
                        piecemoves.Add(wdiag1);
                    if (ai.IsValidMove(board, wup, color))
                        piecemoves.Add(wup);
                    if (ai.IsValidMove(board, wdiag2, color))
                        piecemoves.Add(wdiag2);
                    break;
                case ChessPiece.BlackKing:
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            move = new ChessMove(location, new ChessLocation(location.X + i, location.Y + j));
                            if(ai.IsValidMove(board,move,color))
                                piecemoves.Add(move);
                        }
                    }
                    break;
                case ChessPiece.WhiteKing:
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            move = new ChessMove(location, new ChessLocation(location.X + i, location.Y + j));
                            if (ai.IsValidMove(board, move, color))
                                piecemoves.Add(move);
                        }
                    }
                    break;
                case ChessPiece.BlackKnight:
                    move = new ChessMove(location, new ChessLocation(location.X + 2, location.Y + 1));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 2, location.Y + -1));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 1, location.Y + 2));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 1, location.Y + -2));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -2, location.Y + 1));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -2, location.Y + -1));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -1, location.Y + 2));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -1, location.Y + -2));
                    if(ai.IsValidMove(board,move,color))
                        piecemoves.Add(move);
                    break;
                case ChessPiece.WhiteKnight:
                    move = new ChessMove(location, new ChessLocation(location.X + 2, location.Y + 1));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 2, location.Y + -1));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 1, location.Y + 2));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + 1, location.Y + -2));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -2, location.Y + 1));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -2, location.Y + -1));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -1, location.Y + 2));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    move = new ChessMove(location, new ChessLocation(location.X + -1, location.Y + -2));
                    if (ai.IsValidMove(board, move, color))
                        piecemoves.Add(move);
                    break;
                default:
                    return piecemoves;
            }
            return piecemoves;
        }
    }
}
