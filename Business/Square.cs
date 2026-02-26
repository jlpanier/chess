using static Business.Piece;

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

        public string Position => $"{(char)('a' + Column)}{Row+1}";

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

        public static Square Copy(Square source) => new Square(source.Index)
        {
            IsAuthorizedMove = source.IsAuthorizedMove,
            IsBadMove= source.IsBadMove,
            IsSelected = source.IsSelected,
            IsBestMove = source.IsBestMove,
            IsWarning = source.IsWarning,
            Piece = source.Piece == null ? null : Piece.Copy(source.Piece),
        };

        /// <summary>
        /// Sélection de la pièce
        /// </summary>
        public void Select()
        {
            IsSelected = true;
       }

        /// <summary>
        /// Désélection de la pièce
        /// </summary>
        public void Unselect()
        {
            IsSelected = false;
            IsWarning = false;
            IsBadMove = false;
            IsBestMove = false;
            IsAuthorizedMove = false;
        }

        /// <summary>
        /// Meilleur coup possible pour la pièce sélectionnée
        /// </summary>
        public bool IsBestMove { get; set; }

        /// <summary>
        /// Attention, ce coup est dangereux
        /// </summary>
        public bool IsWarning { get; set; }

        /// <summary>
        /// Mauvais coup, à éviter
        /// </summary>
        public bool IsBadMove { get; set; }

        /// <summary>
        /// Mouvement autorisé pour la pièce sélectionnée
        /// </summary>
        public bool IsAuthorizedMove { get; set; }
    }

}