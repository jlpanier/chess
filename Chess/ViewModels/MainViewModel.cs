using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using static Business.Piece;

namespace Chess.ViewModels
{
    /// <summary>
    /// Gestion de la vue principale
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Echiquier
        /// </summary>
        private readonly Board _board;

        #region Propriétés

        /// <summary>
        /// Case sélectionnée
        /// </summary>
        public SquareViewModel? Selected => Squares.FirstOrDefault(s => s.IsSelected);

        /// <summary>
        /// Désigne la couleur du joueur qui doit jouer
        /// </summary>
        public PieceColor Playing => _board.Playing;

        #endregion

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
        /// Retourne la case de l'échiquier par son index
        /// </summary>
        public Square GetSquare(int index) => _board.Squares.First(s => s.Index == index);

        /// <summary>
        /// Retourne la case de l'échiquier sélectionnée
        /// </summary>
        public Square? GetSelected() => _board.Squares.FirstOrDefault(s => s.IsSelected);

        /// <summary>
        /// Désélection d'une case de l'échiquier
        /// </summary>
        public void Unselect()
        {
            foreach (var square in Squares)
            {
                square.IsSelected = false;
            }
        }

        /// <summary>
        /// Effectue le mouvement
        /// </summary>
        public bool Move(Square from, Square to) => _board.Move(from, to);
    }
}
