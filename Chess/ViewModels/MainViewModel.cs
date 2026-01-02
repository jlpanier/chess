using Business;
using Chess.Interfaces;
using Chess.Pages;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Mvvm;
using FFImageLoading.Helpers;
using Repository.Dbo;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Chess.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly Board _board;

        public ObservableCollection<BoardSquareViewModel> Squares { get; } =
            new ObservableCollection<BoardSquareViewModel>();

        [ObservableProperty] bool whiteToMove;
        [ObservableProperty] string statusText;

        private int? _selectedIndex;
        private List<Move> _currentLegalMoves = new();

        public MainViewModel()
        {
            _board = new Board();
            InitSquares();
            NewGame();
        }
        public MainViewModel(Board board)
        {
            _board = board;
            InitSquares();
            NewGame();
        }

        void InitSquares()
        {
            for (int i = 0; i < 64; i++)
                Squares.Add(new BoardSquareViewModel { Index = i });
        }

        [RelayCommand]
        void NewGame()
        {
            _board.SetupInitialPosition();
            whiteToMove = true;
            RefreshSquares();
            statusText = "New game started";
        }

        [RelayCommand]
        void SquareTapped(BoardSquareViewModel square)
        {
            if (_selectedIndex == null)
            {
                SelectFrom(square);
            }
            else
            {
                TryMoveTo(square);
            }
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
                vm.IsHighlighted = false;
                vm.IsSelected = false;
            }
        }

        void HighlightSelection()
        {
            foreach (var s in Squares)
            {
                s.IsSelected = (s.Index == _selectedIndex);
                s.IsHighlighted = _currentLegalMoves.Any(m => m.To == s.Index);
            }
        }

        void ClearSelection()
        {
            _selectedIndex = null;
            _currentLegalMoves.Clear();
            foreach (var s in Squares)
            {
                s.IsSelected = false;
                s.IsHighlighted = false;
            }
        }

        string? PieceToSymbol(Piece? piece)=> PieceSymbols.ToSymbol(piece);

        void UpdateStatus()
        {
            // check / checkmate / stalemate text
        }
    }

}
