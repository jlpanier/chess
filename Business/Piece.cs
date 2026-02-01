namespace Business
{
    /// <summary>
    /// Gestion de la pièce de l'échiquier
    /// </summary>
    public class Piece
    {
        public override string ToString() => $"{Color} {Type}";

        /// <summary>
        /// Types de pièces disponibles
        /// </summary>
        public enum PieceType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn
        }

        /// <summary>
        /// Couleurs des pièces
        /// </summary>
        public enum PieceColor
        {
            White,
            Black
        }

        /// <summary>
        /// Types de pièce
        /// </summary>
        public PieceType Type { get; }

        /// <summary>
        /// Couleur de la pièce
        /// </summary>
        public PieceColor Color { get; }

        /// <summary>
        /// VRAI, si la pièce a déjà bougé
        /// </summary>
        public bool HasMoved { get; }

        /// <summary>
        /// VRAI, si la pièce est de couleur blanche
        /// </summary>
        public bool IsWhite => Color == PieceColor.White;

        /// <summary>
        /// VRAI, si la pièce est de couleur noire
        /// </summary>
        public bool IsBlack => Color == PieceColor.Black;

        public Piece(PieceType type, PieceColor color, bool hasMoved = false)
        {
            Type = type;
            Color = color;
            HasMoved = hasMoved;
        }

        /// <summary>
        /// VRAI, si la pièce de même couleur que l'autre pièce
        /// </summary>
        public bool IsSameColor(Piece piece) => IsSameColor(piece.Color);

        /// <summary>
        /// VRAI, si la pièce de même couleur que l'autre pièce
        /// </summary>
        public bool IsSameColor(PieceColor color) => Color == color;
    }

}
