using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using static Business.Piece;

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
        public bool IsAuthorizedMove
        {
            get => _isPossibleMove;
            set
            {
                if (_isPossibleMove != value)
                {
                    _isPossibleMove = value;
                    OnPropertyChanged(nameof(IsAuthorizedMove));
                }
            }
        }
        private bool _isPossibleMove;

        public Color BorderMoveColor
        {
            get => _borderMoveColor ?? Colors.Transparent;
            private set
            {
                if (_borderMoveColor != value)
                {
                    _borderMoveColor = value;
                    OnPropertyChanged(nameof(BorderMoveColor));
                }
            }
        }
        private Color? _borderMoveColor;

        public Color MoveColor
        {
            get => _moveColor ?? Colors.Transparent;
            private set
            {
                if (_moveColor != value)
                {
                    _moveColor = value;
                    OnPropertyChanged(nameof(MoveColor));
                }
            }
        }
        private Color? _moveColor;

        public bool ShowBorder
        {
            get => _isBestMoveTarget;
            private set
            {
                if (_isBestMoveTarget != value)
                {
                    _isBestMoveTarget = value;
                    OnPropertyChanged(nameof(ShowBorder));
                }
            }
        }
        private bool _isBestMoveTarget;

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

        [RelayCommand]
        public void OnDoubleClickSquare()
        {
            _boardViewModel.DoubleClick(Index);
        }

        public void Hint()
        {
            if (Square.IsWarning)
            {
                BorderMoveColor = Colors.Red;
            }
            else if (Square.IsBestMove)
            {
                BorderMoveColor = Colors.LimeGreen;
            }
            else
            {
                BorderMoveColor = Colors.Transparent;
            }
            PieceColor = Square.HasPiece ? (Square.Piece!.IsWhite ? Colors.White : Colors.Black) : Colors.Transparent;
            PieceSymbol = Square.Piece != null ? Square.Piece.ToPieceSymbol() : string.Empty;
            IsSelected = Square.IsSelected;
            MoveColor = Square.IsBadMove ? Colors.Red : Square.IsAuthorizedMove ? Colors.LightGreen : Colors.Transparent;
            ShowBorder = BorderMoveColor != Colors.Transparent;
            IsAuthorizedMove = MoveColor != Colors.Transparent;
        }
    }
}
