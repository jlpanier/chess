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
        /// Commande exécutée lorsqu'une case est tapée.
        /// </summary>
        [RelayCommand]
        private void SquareTapped(SquareViewModel item)
        {
            // Exemple simple : sélectionne la case
            item.Selected();

            // Si tu veux désélectionner les autres cases :
            // foreach (var sq in Squares)
            //     if (sq != item) sq.Unselected();
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
                items.Add(new SquareViewModel(_board, square));
            }
            Squares = new ObservableCollection<SquareViewModel>(items);
        }
    }
}
