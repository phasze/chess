/******************************************************************************
* The MIT License
* Copyright (c) 2008 Rusty Howell, Thomas Wiest
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the Software), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*******************************************************************************/

// Authors:
// 		Thomas Wiest  twiest@users.sourceforge.net
//		Rusty Howell  rhowell@users.sourceforge.net

namespace UvsChess
{
    /// <summary>
    /// An enum that represents all of the possible chess pieces (including their colors), and also an empty tile.
    /// The order of this enum was chosen on purpose. See the comments in ChessPiece.cs for more information.
    /// </summary>
    public enum ChessPiece
    {
        // We chose this order for Chess Piece on purpose.
        // Basically anything < ChessPiece.Empty means that the piece is black and 
        // anything > ChessPiece.Empty means that the piece is white.
        // This makes it _very_ quick and easy to see what color a piece at a certain location is.
        BlackPawn,
        BlackRook,
        BlackKnight,
        BlackBishop,
        BlackQueen,
        BlackKing,

        Empty,

        WhitePawn,
        WhiteRook,
        WhiteKnight,
        WhiteBishop,
        WhiteQueen,
        WhiteKing
    }
    internal sealed class Piece
    {
        #region InternalMembers

        internal ChessColor PieceColor;
        internal ChessPiece PieceType;

        internal short PieceValue;
        internal short PieceActionValue;

        internal short AttackedValue;
        internal short DefendedValue;

        internal int LastValidMoveCount;
        internal bool Moved;

        internal bool Selected;
        
        #endregion

        #region Constructors

        internal Piece(Piece piece)
        {
            PieceColor = piece.PieceColor;
            PieceType = piece.PieceType;
            Moved = piece.Moved;
            PieceValue = piece.PieceValue;
            PieceActionValue = piece.PieceActionValue;

            if (piece.ValidMoves != null)
                LastValidMoveCount = piece.ValidMoves.Count;
        }

        internal Piece(ChessPiece chessPiece, ChessColor chessPieceColor)
        {
            PieceType = chessPiece;
            PieceColor = chessPieceColor;


            PieceValue = CalculatePieceValue(PieceType);
            PieceActionValue = CalculatePieceActionValue(PieceType);
        }

        #endregion

        #region PrivateMethods

        private static short CalculatePieceValue(ChessPiece pieceType)
        {
            switch (pieceType)
            {
                case ChessPiece.BlackPawn:
                case ChessPiece.WhitePawn:
                    {
                        return 100;

                    }
                case ChessPiece.BlackKnight:
                case ChessPiece.WhiteKnight:
                    {
                        return 320;
                    }
                case ChessPiece.BlackBishop:
                case ChessPiece.WhiteBishop:
                    {
                        return 325;
                    }
                case ChessPiece.BlackRook:
                case ChessPiece.WhiteRook:
                    {
                        return 500;
                    }

                case ChessPiece.BlackQueen:
                case ChessPiece.WhiteQueen:
                    {
                        return 975;
                    }

                case ChessPiece.BlackKing:
                case ChessPiece.WhiteKing:
                    {
                        return 3000;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }


        private static short CalculatePieceActionValue(ChessPiece pieceType)
        {
            switch (pieceType)
            {
                case ChessPiece.BlackPawn:
                case ChessPiece.WhitePawn:
                    {
                        return 6;

                    }
                case ChessPiece.BlackKnight:
                case ChessPiece.WhiteKnight:
                    {
                        return 3;
                    }
                case ChessPiece.BlackBishop:
                case ChessPiece.WhiteBishop:
                    {
                        return 3;
                    }
                case ChessPiece.BlackRook:
                case ChessPiece.WhiteRook:
                    {
                        return 2;
                    }

                case ChessPiece.BlackQueen:
                case ChessPiece.WhiteQueen:
                    {
                        return 1;
                    }

                case ChessPiece.BlackKing:
                case ChessPiece.WhiteKing:
                    {
                        return 1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        #endregion
    }
}