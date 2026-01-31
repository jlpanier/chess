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

        public Square Square => _boardViewModel.Board.GetSquare(Index);

        /// <summary>
        /// Référence de l'index de la case
        /// </summary>
        public int Index
        {
            get => _index ?? 0;
            set
            {
                if (_index != value)
                {
                    _index = value;
                }
            }
        }
        private int? _index;

        /// <summary>
        /// Colonne de la case (0 à 7)
        /// </summary>
        public int Column
        {             
            get => _column ?? 0;
            set
            {
                if (_column != value)
                {
                    _column = value;
                }
            }
        }
        private int? _column;

        /// <summary>
        /// rangée de la case (0 à 7)
        /// </summary>
        public int Row
        {
            get => _row ?? 0;
            set
            {
                if (_row != value)
                {
                    _row = value;
                }
            }
        }
        private int? _row;

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
            get => _pieceColor ?? Colors.Transparent;
            set
            {
                if (_pieceColor != value)
                {
                    _pieceColor = value;
                    OnPropertyChanged(nameof(PieceColor));
                }
            }
        }
        private Color? _pieceColor;

        /// <summary>
        /// Affichage de la pièce
        /// </summary>
        public string PieceSymbol
        {
            get => _pieceSymbol;
            set
            {
                if (_pieceSymbol!=value)
                {
                    _pieceSymbol = value;
                    OnPropertyChanged(nameof(PieceSymbol));
                }
            }
        }
        private string _pieceSymbol="";

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
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        private bool _isSelected;

        /// <summary>
        /// VRAI, si la pièce sélectionnée peut se déplacer à cet endroit
        /// </summary>
        public bool IsPossibleMove
        {
            get => _isPossibleMove;
            set
            {
                if (_isPossibleMove != value)
                {
                    _isPossibleMove = value;
                    OnPropertyChanged(nameof(IsPossibleMove));
                }
            }
        }
        private bool _isPossibleMove;

        #endregion

        public SquareViewModel(MainViewModel board, Square square)
        {
            Index = square.Index;
            Row = square.Row;
            Column = square.Column;
            PieceSymbol = square.Piece?.ToPieceSymbol() ?? string.Empty;
            PieceColor = square.HasPiece ? (square.Piece!.IsWhite ? Colors.White : Colors.Black) : Colors.Transparent;
            IsSelected = false;
            _boardViewModel = board;
        }

        /// <summary>
        /// L'utilisateur a clické sur la case
        /// </summary>
        [RelayCommand]
        public void OnSelectSquare()
        {
            _boardViewModel.Select(Index);
        }

        /// <summary>
        /// Sélection de la case
        /// </summary>
        public void Select()
        {
            IsSelected = true; // Indiquer à la vue la sélection
            Square.Select(); // Indiquer à la classe business la sélection
        }

        /// <summary>
        /// Déselection de la case
        /// </summary>
        public void Unselect()
        {
            IsSelected = false; // Indiquer à la vue la désélection
            Square.Unselect(); // Indiquer à la classe business la sélection
        }

        /// <summary>
        /// La case est vide
        /// </summary>
        public void Empty()
        {
            PieceColor = Colors.Transparent;
            PieceSymbol = string.Empty;
        }

        /// <summary>
        /// Positionner une pièce sur la case
        /// </summary>
        public void Set(Piece piece)
        {
            PieceColor = piece!.IsWhite ? Colors.White : Colors.Black;
            PieceSymbol = piece.ToPieceSymbol();
        }
    }
}
