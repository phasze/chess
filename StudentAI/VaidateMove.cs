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
        /// <returns></returns>
        public static bool WhiteKing(ChessBoard board, ChessMove move)
        {
            //TODO NOT DONE, still need to finish this
            //validate the move is still on the board
            if (!StillOnBoard(move.To.X, move.To.Y))
                return false;

            //validate it doesn't put self into check


        }

        //checks if the move is still located on the board
        private static bool StillOnBoard(int X, int Y)
        {
            if (X < 8 && X > -1 && Y < 8 && Y > -1)
                return true;
            return false;
        }
    }
}
