using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Core.Internal;
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
                }
            }
        }
        private string? _pieceSymbol;

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

        #endregion

        public SquareViewModel(MainViewModel board, Square square)
        {
            Index = square.Index;
            Row = square.Row;
            Column = square.Column;
            PieceSymbol = square.Piece?.ToPieceSymbol() ?? string.Empty;
            PieceColor = GetColor(square);
            IsSelected = false;
            _boardViewModel = board;
        }

        /// <summary>
        /// L'utilisateur a clické sur la case
        /// </summary>
        [RelayCommand]
        public void OnSelectSquare()
        {
            var select = _boardViewModel.GetSquare(Index);
            var previous = _boardViewModel.GetSelected();
            if (previous == null)
            {
                if (select.Piece != null && select.Piece.IsSameColor(_boardViewModel.Playing))
                {
                    IsSelected = true;
                    select.Selected();
                }
            }
            else
            {
                if (_boardViewModel.Move(previous, select))
                {
                    PieceSymbol = previous.Piece?.ToPieceSymbol() ?? string.Empty;
                    PieceColor = GetColor(previous);
                    var previousViewModel = _boardViewModel.Selected;
                    if(previousViewModel!=null)
                    {
                        previousViewModel.PieceColor = Colors.Transparent;
                        previousViewModel.PieceSymbol = string.Empty;
                    }
                }
                previous.Unselect();
                _boardViewModel.Unselect();
            }

        }

        /// <summary>
        /// Retourne la couleur de la pièce
        /// </summary>
        public static Color GetColor(Square square) => square.HasPiece ? (square.Piece!.IsWhite ? Colors.White : Colors.Black) : Colors.Transparent;

    }
}
