using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business
{
    /// <summary>
    /// Information sur le déplacement des pièces
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Déplacement référence
        /// </summary>
        public readonly int Number;

        /// <summary>
        /// Case de départ
        /// </summary>
        public readonly Square From;

        /// <summary>
        /// Case d'arrivée
        /// </summary>
        public readonly Square To;

        /// <summary>
        /// Vrai si mouvement roque côté du roi
        /// </summary>
        public readonly bool IsCastleKingSide;

        /// <summary>
        /// Vrai si mouvement roque côté du reine
        /// </summary>
        public readonly bool IsCastleQueenSide;

        /// <summary>
        /// Vrai si mouvement capture une pièce
        /// </summary>
        private readonly bool IsCapture;

        /// <summary>
        /// Vrai si mouvement d'un pion en dernière ligne donne une promotion
        /// </summary>
        private readonly bool IsPromotion;

        /// <summary>
        /// Déclaration du mouvement
        /// </summary>
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

        /// <summary>
        /// Affichage du mouvement
        /// </summary>
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

