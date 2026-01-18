using static Business.Piece;

namespace Business
{
    public static class Extensions
    {
        /// <summary>
        /// Affichage de la pièce sous forme de symbole Unicode.
        /// </summary>
        public static string ToPieceSymbol(this Piece piece)
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
