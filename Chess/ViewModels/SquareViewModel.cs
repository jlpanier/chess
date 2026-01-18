using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chess.ViewModels
{
    /// <summary>
    /// Gestion d'une case de l'échiquier
    /// </summary>
    public partial class SquareViewModel : ObservableObject
    {
        #region Properties

        /// <summary>
        /// Référence à l'échiquier principal
        /// </summary>
        private readonly MainViewModel _boardViewModel;

        /// <summary>
        /// Gestion de la case de l'échiquier
        /// </summary>
        public Square Square { get; private set; }

        /// <summary>
        /// Référence de l'index de la case
        /// </summary>
        public int Index => Square.Index;

        /// <summary>
        /// Est-elle occupée par une pièce ?
        /// </summary>
        public bool HasPiece => Square.Piece != null;

        /// <summary>
        /// Colonne de la case (0 à 7)
        /// </summary>
        public int Column => Index % 8;

        /// <summary>
        /// rangée de la case (0 à 7)
        /// </summary>
        public int Row => (int)(Index / 8);

        /// <summary>
        /// Première colonne
        /// </summary>
        public bool IsFirstColumn => Column == 0;

        /// <summary>
        /// Première rangée
        /// </summary>
        public bool IsFirstRow => Row == 0;

        /// <summary>
        /// Nommée la rangée
        /// </summary>
        public string LetterRow => "12345678"[Row].ToString();

        /// <summary>
        /// Nommée la colonne
        /// </summary>
        public string LetterColumn => "abcdefgh"[Column].ToString();

        /// <summary>
        /// Couleur de la pièce sur la case
        /// </summary>
        public Color PieceColor
        {
            get
            {
                if (Square.Piece != null)
                {
                    return Square.Piece.IsWhite ? Colors.White : Colors.Black;
                }
                return Colors.Black;
            }
        }

        /// <summary>
        /// Affichage de la pièce
        /// </summary>
        public string PieceSymbol => PieceSymbols.ToSymbol(Square?.Piece);

        /// <summary>
        /// VRAI, si la pièce est sélectionnée
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    if (value) Square.Selected(); else Square.Unselected();
                    OnPropertyChanged();
                }
            }
        }
        private bool _isSelected;

        #endregion

        public SquareViewModel(MainViewModel board, Square square)
        {
            Square = square;
            _boardViewModel = board;
        }

        /// <summary>
        /// L'utilisateur a clické sur la case
        /// </summary>
        [RelayCommand]
        public void SelectSquare()
        {
            IsSelected = true;
            _boardViewModel.Select(this);
        }
    }
}
