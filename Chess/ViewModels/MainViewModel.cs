using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Chess.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        private readonly Board _board;

        [ObservableProperty]
        public ObservableCollection<BoardSquareViewModel> squares;

        [ObservableProperty] bool whiteToMove;

        private int? _selectedIndex;
        private List<Move> _currentLegalMoves = new();

        public MainViewModel()
        {
            _board = new Board();
            Squares = new ObservableCollection<BoardSquareViewModel>();
            for (int i = 0; i < 64; i++)
                Squares.Add(new BoardSquareViewModel 
                { 
                    Index = i,
                    PieceSymbol= GetInitialPiece(i),
                    IsWhitePiece= i > 32
                });
        }

        [RelayCommand]
        private void OnSquareTapped(BoardSquareViewModel square)
        {
            // Clear previous selection
            foreach (var sq in Squares)
                sq.IsSelected = false;

            // Select the tapped one
            square.IsSelected = true;
        }

        private string GetInitialPiece(int index)
        {
            // Black pieces
            string[] backRank = { "♜", "♞", "♝", "♛", "♚", "♝", "♞", "♜" };

            int row = index / 8;
            int col = index % 8;

            return row switch
            {
                0 => backRank[col],   // black major pieces
                1 => "♟",             // black pawns
                6 => "♙",             // white pawns
                7 => backRank[col].Replace('♜', '♖')
                                   .Replace('♞', '♘')
                                   .Replace('♝', '♗')
                                   .Replace('♛', '♕')
                                   .Replace('♚', '♔'), // white major pieces
                _ => ""
            };
        }

        void InitSquares()
        {
            for (int i = 0; i < 64; i++)
                Squares.Add(new BoardSquareViewModel { Index = i });
        }

        [RelayCommand]
        void NewGame()
        {
            //_board.SetupInitialPosition();
            whiteToMove = true;
            //RefreshSquares();
        }

        void SelectFrom(BoardSquareViewModel square)
        {
            var piece = _board.GetPiece(square.Index);
            //if (piece is null || piece.Color != (whiteToMove ? Color.White : Color.Black))
            //    return;

            _selectedIndex = square.Index;
            _currentLegalMoves = _board.GetLegalMoves(square.Index).ToList();
            HighlightSelection();
        }

        void TryMoveTo(BoardSquareViewModel target)
        {
            var move = _currentLegalMoves.FirstOrDefault(m => m.To == target.Index);
            if (move is null)
            {
                ClearSelection();
                return;
            }

            _board.ApplyMove(move);
            whiteToMove = !whiteToMove;
            RefreshSquares();
            ClearSelection();
            UpdateStatus();
        }

        void RefreshSquares()
        {
            for (int i = 0; i < 64; i++)
            {
                var piece = _board.GetPiece(i);
                var vm = Squares[i];
                vm.PieceSymbol = PieceToSymbol(piece);
                vm.IsSelected = false;
            }
        }

        void HighlightSelection()
        {
            foreach (var s in Squares)
            {
                s.IsSelected = (s.Index == _selectedIndex);
            }
        }

        void ClearSelection()
        {
            _selectedIndex = null;
            _currentLegalMoves.Clear();
            foreach (var s in Squares)
            {
                s.IsSelected = false;
            }
        }

        string? PieceToSymbol(Piece? piece)=> PieceSymbols.ToSymbol(piece);

        void UpdateStatus()
        {
            // check / checkmate / stalemate text
        }
    }

}
