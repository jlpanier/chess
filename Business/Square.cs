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
        }
    }
}