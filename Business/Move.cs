using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Piece;

namespace Business
{
    public class Move
    {
        public enum MoveType
        {
            Normal,
            Capture,
            EnPassant,
            CastleKingSide,
            CastleQueenSide,
            Promotion,
            PromotionCapture
        }


        public int From { get; }
        public int To { get; }
        public PieceType? Promotion { get; }
        public bool IsCapture { get; }
        public bool IsEnPassant { get; }
        public bool IsCastleKingSide { get; }
        public bool IsCastleQueenSide { get; }

        public Move(
            int from,
            int to,
            PieceType? promotion = null,
            bool isCapture = false,
            bool isEnPassant = false,
            bool isCastleKingSide = false,
            bool isCastleQueenSide = false)
        {
            From = from;
            To = to;
            Promotion = promotion;
            IsCapture = isCapture;
            IsEnPassant = isEnPassant;
            IsCastleKingSide = isCastleKingSide;
            IsCastleQueenSide = isCastleQueenSide;
        }

        public override string ToString()
        {
            string promo = Promotion.HasValue ? $"={Promotion.Value}" : "";
            return $"{From}->{To}{promo}";
        }
    }
}
