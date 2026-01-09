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
                { Color: PieceColor.White, Type: PieceType.King } => "♔",
                { Color: PieceColor.White, Type: PieceType.Queen } => "♕",
                { Color: PieceColor.White, Type: PieceType.Rook } => "♖",
                { Color: PieceColor.White, Type: PieceType.Bishop } => "♗",
                { Color: PieceColor.White, Type: PieceType.Knight } => "♘",
                { Color: PieceColor.White, Type: PieceType.Pawn } => "♙",

                { Color: PieceColor.Black, Type: PieceType.King } => "♚",
                { Color: PieceColor.Black, Type: PieceType.Queen } => "♛",
                { Color: PieceColor.Black, Type: PieceType.Rook } => "♜",
                { Color: PieceColor.Black, Type: PieceType.Bishop } => "♝",
                { Color: PieceColor.Black, Type: PieceType.Knight } => "♞",
                { Color: PieceColor.Black, Type: PieceType.Pawn } => "♟",

                _ => ""
            };
        }
    }
}
