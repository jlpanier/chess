using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Chess.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly Board _board;

        [ObservableProperty]
        public ObservableCollection<SquareViewModel> squares;

        public MainViewModel()
        {
            _board = new Board();
            squares = new ObservableCollection<SquareViewModel>();
        }

        /// <summary>
        /// Initialisation du jeux - Squares
        /// </summary>
        public void NewGame()
        {
            _board.NewGame();
            var items = new List<SquareViewModel>();
            foreach (var square in _board.Squares)
            {
                items.Add(new SquareViewModel(_board, square));
            }
            Squares = new ObservableCollection<SquareViewModel>(items);
        }
    }
}
