using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Piece;

namespace Business
{
    public static class PieceSymbols
    {
        public static string ToSymbol(Piece? piece)
        {
            return piece switch
            {
                { Type: PieceType.King } => char.ConvertFromUtf32(9818),
                { Type: PieceType.Queen } => "♛",
                { Type: PieceType.Rook } => "♜",
                { Type: PieceType.Bishop } => "♝",
                { Type: PieceType.Knight } => "♞",
                { Type: PieceType.Pawn } => "♟",

                _ => ""
            };
        }
    }
}
