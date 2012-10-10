using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentAI
{
    class Hueristic
    {
        public UvsChess.ChessBoard BoardAfterMove = null;
        public UvsChess.ChessMove TheMove=null;
        public Hueristic(UvsChess.ChessBoard board, UvsChess.ChessMove move)
        {
            BoardAfterMove = board;
            BoardAfterMove.MakeMove(move);
            TheMove = move;
        }
    }
}
