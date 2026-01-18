namespace Business
{
    /// <summary>
    /// Gestion d'une case de l'échiquier
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Référence de la case de 0 à 63
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Colonne de la case (0 à 7)
        /// </summary>
        public int Column => Index % 8;

        /// <summary>
        /// rangée de la case (0 à 7)
        /// </summary>
        public int Row => (int)(Index / 8);

        /// <summary>
        /// Est-elle occupée par une pièce ?
        /// </summary>
        public bool HasPiece => Piece != null;

        /// <summary>
        /// VRAI, si la pièce est sélectionnée
        /// </summary>
        public bool IsSelected { get; private set; }

        /// <summary>
        /// Pièce occupée sur cette case de l'échiquier
        /// </summary>
        public Piece? Piece { get; set; }

        public Square(int index)
        {
            Index = index;
            Piece = null;
        }

        public Square(int index, Piece piece)
        {
            Index = index;
            Piece = piece;
        }

        /// <summary>
        /// Sélection de la pièce
        /// </summary>
        public void Selected()
        {
            IsSelected = true;
        }

        /// <summary>
        /// Désélection de la pièce
        /// </summary>
        public void Unselect()
        {
            IsSelected = false;
        }

        /// <summary>
        /// VRAI, si la pièce peut bouger à to 
        /// </summary>
        public bool IsValidMove(Square to)
        {
            System.Diagnostics.Debug.Assert(Piece != null);

            var result = true;
            switch (Piece.Type)
            {
                case Piece.PieceType.King:
                    return IsValidKingMove(to);
                case Piece.PieceType.Queen:
                    break;
                case Piece.PieceType.Rook:
                    break;
                case Piece.PieceType.Bishop:
                    break;
                case Piece.PieceType.Knight:
                    break;
                case Piece.PieceType.Pawn:
                    break;
            }
            return result;
        }

        /// <summary>
        /// VRAI, si le roi peut bouger à to 
        /// </summary>
        private bool IsValidKingMove(Square to)
        {
            System.Diagnostics.Debug.Assert(Piece != null);

            if (to.HasPiece && Piece.IsSameColor(to))
            {
                return false;
            }
            if(to.Row == Row && to.Column == Column)
            {
                return false;
            }
            if (Math.Abs(to.Row - Row) > 1)
            {
                return false;
            }
            if (Math.Abs(to.Column - Column) > 1)
            {
                return false;
            }
            // TODO: Vérifier si pas echec
            return true;
        }
    }
}