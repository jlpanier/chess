using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Piece
    {
        public enum PieceType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn
        }

        public enum PieceColor
        {
            White,
            Black
        }

        public PieceType Type { get; }
        public PieceColor Color { get; }
        public bool HasMoved { get; }

        public bool IsWhite => Color == PieceColor.White;

        public bool IsBlack => Color == PieceColor.Black;

        public Piece(PieceType type, PieceColor color, bool hasMoved = false)
        {
            Type = type;
            Color = color;
            HasMoved = hasMoved;
        }

        // Create a new instance with HasMoved = true
        public Piece MarkMoved() =>
            new Piece(Type, Color, hasMoved: true);

        public override string ToString() =>
            $"{Color} {Type}";
    }

}
