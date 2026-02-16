using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business
{
    public class Move
    {
        public readonly int Number;

        public readonly Square From;

        public readonly Square To;

        private readonly bool IsCastleKingSide;

        private readonly bool IsCastleQueenSide;

        private readonly bool IsCapture;

        private readonly bool IsPromotion;

        public Move(int moveno, Square from, Square to)
        {
            Number  = moveno;
            From = Square.Copy(from);
            To = Square.Copy(to);

            System.Diagnostics.Debug.Assert(From.Piece != null);
            System.Diagnostics.Debug.Assert(To != null);

            IsCastleKingSide = From.Piece.Type == Piece.PieceType.King && From.Column - To.Column > 1;
            IsCastleQueenSide = From.Piece.Type == Piece.PieceType.King && To.Column - From.Column > 1;
            IsCapture = To.Piece != null;
            IsPromotion = From.Piece.Type == Piece.PieceType.Pawn && (To.Row == 0 || To.Row == 7);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_toString))
            {
                if (IsCastleKingSide)
                {
                    _toString = "O-O";
                }
                else if (IsCastleQueenSide)
                {
                    _toString = "O-O-O";
                }
                else
                {
                    string capture = IsCapture ? "x" : "-";
                    string promo = IsPromotion && To.Piece != null ? $"={To.Piece.ToPieceSymbol()}" : "";

                    System.Diagnostics.Debug.Assert(From.Piece != null);
                    _toString = $"{From.Piece.ToLetterPiece()}{From.Position}{capture}{To.Position}{promo}";
                }
            }
            return _toString;
        }
        private string _toString = "";

    }
}

