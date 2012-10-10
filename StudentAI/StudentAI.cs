using System;
using System.Collections.Generic;
using System.Text;
using UvsChess;

namespace StudentAI
{
    public class StudentAI : IChessAI
    {
        #region IChessAI Members that are implemented by the Student
        Random rand = new Random();

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
            List<ChessMove> allmoves = new List<ChessMove>();
            allmoves.AddRange(PieceMoves.getmovesofcolor(this, myColor, board));
            List<Hueristic> HueristicMoves = new List<Hueristic>();
            //this is where most of our work will be... :'(

            ChessColor oppositeColor = myColor == ChessColor.Black ? ChessColor.White : ChessColor.Black;

            //TODO possibly temorary, but set the check flag
            foreach (var move in allmoves)
            {
                var tempBoard = board.Clone();
                tempBoard.MakeMove(move);

                if (IsKingInCheck(tempBoard, oppositeColor))
                    move.Flag = ChessFlag.Check;

                HueristicMoves.Add(new Hueristic(board, move, oppositeColor));
            }

            HueristicMoves.Sort((x, y) => x.HValue.CompareTo(y.HValue));
            
            if (allmoves.Count == 0)
            {
                var game_over = new ChessMove(null, null);
                game_over.Flag = ChessFlag.Stalemate;
                return game_over;
                
            }
            //var index = rand.Next(0, allmoves.Count);
            var index = 0;
            if (HueristicMoves[0].HValue == HueristicMoves[HueristicMoves.Count - 1].HValue)
            {
                index = rand.Next(0, HueristicMoves.Count);
            }
            //check if opponent is in checkmate
            if (HueristicMoves[index].TheMove.Flag == ChessFlag.Check)
                if (PieceMoves.getmovesofcolor(this, oppositeColor, HueristicMoves[index].BoardAfterMove).Count == 0)
                    HueristicMoves[index].TheMove.Flag = ChessFlag.Checkmate;

            return HueristicMoves[index].TheMove;
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
            if (moveToCheck.To.X > 7 || moveToCheck.To.X < 0 || moveToCheck.To.Y > 7 || moveToCheck.To.Y < 0)
                return false;

            //if it didn't move at all
            if (moveToCheck.To.X == moveToCheck.From.X && moveToCheck.To.Y == moveToCheck.From.Y)
                return false;

            // Check if the colorOfPlayerMoving is in CHECK after this move
            var newBoard = boardBeforeMove.Clone();
            newBoard.MakeMove(moveToCheck);
            //bloody taking away the king breaks stuff in my iskingincheck function
            if (boardBeforeMove[moveToCheck.To.X,moveToCheck.To.Y]==ChessPiece.BlackKing ||
                boardBeforeMove[moveToCheck.To.X, moveToCheck.To.Y] == ChessPiece.WhiteKing ||
                IsKingInCheck(newBoard, colorOfPlayerMoving))
                return false;

            //if they say it's check let's make sure
            if (moveToCheck.Flag == ChessFlag.Check)
                if (!IsKingInCheck(newBoard, colorOfPlayerMoving == ChessColor.Black ? ChessColor.White : ChessColor.Black))
                    return false;

            //validate color of player moving is the color of the chess piece

            //if they say checkmate but it's actually not
            if (moveToCheck.Flag == ChessFlag.Checkmate)
                if (PieceMoves.getmovesofcolor(this, colorOfPlayerMoving == ChessColor.Black ? ChessColor.White : ChessColor.Black, newBoard).Count != 0)
                    return false;

            switch(piece)
            {
                case ChessPiece.WhiteKing:
                    if (colorOfPlayerMoving != ChessColor.White)
                        return false;
                    return VaidateMove.WhiteKing(boardBeforeMove, moveToCheck);

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

                case ChessPiece.BlackKing:
                    if (colorOfPlayerMoving != ChessColor.Black)
                        return false;
                    return VaidateMove.BlackKing(boardBeforeMove, moveToCheck);

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

        private bool IsKingInCheck(ChessBoard board, ChessColor colorOfKingToCheck)
        {
            //store list of enemy team location and type
            Dictionary<int[],ChessPiece> enemyTeam = new Dictionary<int[], ChessPiece>();
            //List<ChessPiece> enemyTeam = new List<ChessPiece>();

            //store location of King of Piece moving
            int kingX = -1;
            int kingY = -1;

            //populate the enemyTeam list with all enemy pieces
            for(int x=0;x<8;x++)
            {
                for(int y=0;y<8;y++)
                {
                    if (colorOfKingToCheck == ChessColor.White)
                    {
                        if(board[x,y]==ChessPiece.WhiteKing)
                        {
                            kingX = x;
                            kingY = y;
                        }
                        switch (board[x, y])
                        {
                            case ChessPiece.BlackPawn:
                            case ChessPiece.BlackKnight:
                            case ChessPiece.BlackBishop:
                            case ChessPiece.BlackQueen:
                            case ChessPiece.BlackRook:
                            case ChessPiece.BlackKing:
                                enemyTeam.Add(new[]{x,y}, board[x,y]);
                                break;
                        }
                    }
                    if (colorOfKingToCheck == ChessColor.Black)
                    {
                        if(board[x,y]==ChessPiece.BlackKing)
                        {
                            kingX = x;
                            kingY = y;
                        }
                        switch (board[x, y])
                        {
                            case ChessPiece.WhiteBishop:
                            case ChessPiece.WhiteKing:
                            case ChessPiece.WhiteKnight:
                            case ChessPiece.WhitePawn:
                            case ChessPiece.WhiteQueen:
                            case ChessPiece.WhiteRook:
                                enemyTeam.Add(new[]{x,y}, board[x,y]);
                                break;
                        }
                    }
                }
            }
            if(kingX==-1 || kingY==-1)
                throw new Exception("King not found...");

            //try and kill the king
            foreach (var chessPiece in enemyTeam)
            {
                switch (chessPiece.Value)
                {
                    case ChessPiece.WhiteBishop:
                        if (VaidateMove.WhiteBishop(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.WhiteKing:
                        if (VaidateMove.WhiteKing(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.WhiteKnight:
                        if (VaidateMove.WhiteKnight(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.WhitePawn:
                        if (VaidateMove.WhitePawn(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.WhiteQueen:
                        if (VaidateMove.WhiteQueen(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.WhiteRook:
                        if (VaidateMove.WhiteRook(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackPawn:
                        if (VaidateMove.BlackPawn(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackKnight:
                        if (VaidateMove.BlackKnight(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackBishop:
                        if (VaidateMove.BlackBishop(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackQueen:
                        if (VaidateMove.BlackQueen(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackRook:
                        if (VaidateMove.BlackRook(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                    case ChessPiece.BlackKing:
                        if (VaidateMove.BlackKing(board, new ChessMove(new ChessLocation(chessPiece.Key[0], chessPiece.Key[1]), new ChessLocation(kingX, kingY))))
                            return true;
                        break;
                }
            }
            return false;
        }

        public class Piece
        {
            #region InternalMembers

            internal ChessColor PieceColor;
            internal ChessPiece PieceType;

            internal short PieceValue;
            internal short PieceActionValue;

            internal short AttackedValue;
            internal short DefendedValue;

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

            public static short CalculatePieceValue(ChessPiece pieceType)
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


            public static short CalculatePieceActionValue(ChessPiece pieceType)
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
