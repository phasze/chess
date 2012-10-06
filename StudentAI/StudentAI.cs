using System;
using System.Collections.Generic;
using System.Text;
using UvsChess;

namespace StudentAI
{
    public class StudentAI : IChessAI
    {
        #region IChessAI Members that are implemented by the Student

        /// <summary>
        /// The name of your AI
        /// </summary>
        public string Name
        {
#if DEBUG
            get { return "StudentAI (Debug)"; }
#else
            get { return "StudentAI"; }
#endif
        }

        /// <summary>
        /// Evaluates the chess board and decided which move to make. This is the main method of the AI.
        /// The framework will call this method when it's your turn.
        /// </summary>
        /// <param name="board">Current chess board</param>
        /// <param name="yourColor">Your color</param>
        /// <returns> Returns the best chess move the player has for the given chess board</returns>
        public ChessMove GetNextMove(ChessBoard board, ChessColor myColor)
        {
            //this is where most of our work will be... :'(
            throw (new NotImplementedException());
        }

        /// <summary>
        /// Validates a move. The framework uses this to validate the opponents move.
        /// </summary>
        /// <param name="boardBeforeMove">The board as it currently is _before_ the move.</param>
        /// <param name="moveToCheck">This is the move that needs to be checked to see if it's valid.</param>
        /// <param name="colorOfPlayerMoving">This is the color of the player who's making the move.</param>
        /// <returns>Returns true if the move was valid</returns>
        public bool IsValidMove(ChessBoard boardBeforeMove, ChessMove moveToCheck, ChessColor colorOfPlayerMoving)
        {
            //possibly switch through pieces to validate the move
            var piece = boardBeforeMove[moveToCheck.From.X, moveToCheck.From.Y];

            //if the piece tried to move off the board...
            if (moveToCheck.To.X > 8 || moveToCheck.To.X < 0 || moveToCheck.To.Y > 8 || moveToCheck.To.Y < 0)
                return false;

            //if it didn't move at all
            if (moveToCheck.To.X == moveToCheck.From.X && moveToCheck.To.Y == moveToCheck.From.Y)
                return false;

            //TODO Check if the colorOfPlayerMoving is in CHECK after this move

            //validate color of player moving is the color of the chess piece

            switch(piece)
            {
                case ChessPiece.WhiteKing:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteKing(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackKing:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.BlackKing(boardBeforeMove, moveToCheck);

                case ChessPiece.WhiteQueen:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteQueen(boardBeforeMove, moveToCheck);

                case ChessPiece.WhitePawn:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhitePawn(boardBeforeMove, moveToCheck);

                case ChessPiece.WhiteRook:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteRook(boardBeforeMove, moveToCheck);

                case ChessPiece.WhiteKnight:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteKnight(boardBeforeMove, moveToCheck);

                case ChessPiece.WhiteBishop:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteBishop(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackPawn:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackPawn(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackKnight:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackKnight(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackBishop:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackBishop(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackQueen:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackQueen(boardBeforeMove, moveToCheck);

                case ChessPiece.BlackRook:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackRook(boardBeforeMove, moveToCheck);

                default:
                    return false;
            }

        }

        #endregion








        #region IChessAI Members that should be implemented as automatic properties and should NEVER be touched by students.
        /// <summary>
        /// This will return false when the framework starts running your AI. When the AI's time has run out,
        /// then this method will return true. Once this method returns true, your AI should return a 
        /// move immediately.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        public AIIsMyTurnOverCallback IsMyTurnOver { get; set; }

        /// <summary>
        /// Call this method to print out debug information. The framework subscribes to this event
        /// and will provide a log window for your debug messages.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="message"></param>
        public AILoggerCallback Log { get; set; }

        /// <summary>
        /// Call this method to catch profiling information. The framework subscribes to this event
        /// and will print out the profiling stats in your log window.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="key"></param>
        public AIProfiler Profiler { get; set; }

        /// <summary>
        /// Call this method to tell the framework what decision print out debug information. The framework subscribes to this event
        /// and will provide a debug window for your decision tree.
        /// 
        /// You should NEVER EVER set this property!
        /// This property should be defined as an Automatic Property.
        /// This property SHOULD NOT CONTAIN ANY CODE!!!
        /// </summary>
        /// <param name="message"></param>
        public AISetDecisionTreeCallback SetDecisionTree { get; set; }
        #endregion
    }
}
