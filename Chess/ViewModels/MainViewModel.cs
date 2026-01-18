using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Chess.ViewModels
{
    /// <summary>
    /// Gestion de la vue principale
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {

        private readonly Board _board;

        [ObservableProperty]
#pragma warning disable MVVMTK0042 // Prefer using [ObservableProperty] on partial properties
        public ObservableCollection<SquareViewModel> squares;
#pragma warning restore MVVMTK0042 // Prefer using [ObservableProperty] on partial properties

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
        public MainViewModel()
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
        {
            _board = new Board();
        }

        /// <summary>
        /// Initialisation du jeux - Squares
        /// </summary>
        public void NewGame()
        {
            _board.NewGame();
            var items = new List<SquareViewModel>();
            if(_board.Squares == null) return;
            foreach (var square in _board.Squares)
            {
                items.Add(new SquareViewModel(this, square));
            }
            Squares = new ObservableCollection<SquareViewModel>(items);
        }

        /// <summary>
        /// Sélection d'une case de l'échiquier
        /// </summary>
        public void Select(SquareViewModel item)
        {
            foreach(var square in Squares)
            {
                square.IsSelected = item == square;
            }
        }
    }
}
