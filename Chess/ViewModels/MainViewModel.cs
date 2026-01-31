using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Mvvm;
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
        public readonly Board Board;

        #region Propriétés

        /// <summary>
        /// Retourne la case de l'échiquier sélectionnée
        /// </summary>
        public SquareViewModel? Selected => Squares.FirstOrDefault(s => s.IsSelected);

        /// <summary>
        /// Retourne la case de l'échiquier par son index
        /// </summary>
        public SquareViewModel GetSquare(int index) => Squares.First(s => s.Index == index);


        #endregion

        [ObservableProperty]
#pragma warning disable MVVMTK0042 // Prefer using [ObservableProperty] on partial properties
        public ObservableCollection<SquareViewModel> squares;
#pragma warning restore MVVMTK0042 // Prefer using [ObservableProperty] on partial properties

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
        public MainViewModel()
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
        {
            Board = new Board();
        }


        /// <summary>
        /// Initialisation du jeux - Squares
        /// </summary>
        public void NewGame()
        {
            Board.NewGame();
            var items = new List<SquareViewModel>();
            if(Board.Squares == null) return;
            foreach (var square in Board.Squares)
            {
                items.Add(new SquareViewModel(this, square));
            }
            Squares = new ObservableCollection<SquareViewModel>(items);
        }


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
        /// Liste des coups possibles pour une pièce
        /// </summary>
        public List<Square> PossibleMoves(SquareViewModel from) => Board.PossibleMoves(from.Square);

        public bool Move(SquareViewModel from, SquareViewModel to) => Board.Move(from.Square, to.Square);
        
        /// <summary>
        /// Sélection d'un case de l'échiquier
        /// </summary>
        public void Select(int index)
        {
            var selected = GetSquare(index);
            if (Selected == null) // Aucune pièce sélectionnée actuellement
            {
                var moves = PossibleMoves(selected);
                if (moves.Any())
                {
                    selected.Select();
                    foreach (var move in moves)
                    {
                        Squares.First(s => s.Index == move.Index).IsPossibleMove = true;
                    }
                }
            }
            else
            {
                var move = PossibleMoves(Selected).FirstOrDefault(_=>_.Index == index);
                if (move != null)
                {
                    if (Board.Move(Selected.Square, move))
                    {
                        var previousSelect = Selected;
                        System.Diagnostics.Debug.Assert(move.Piece != null);
                        selected.Set(move.Piece);
                        previousSelect.Empty();

                        previousSelect.Unselect();
                        selected.Unselect();
                    }
                }
                Squares.ForEach(_ =>
                {
                    _.IsPossibleMove = false;
                    _.Unselect();
                });
            }
        }
    }
}
