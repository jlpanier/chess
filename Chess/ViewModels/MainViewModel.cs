using Business;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.Mvvm;
using System.Collections.ObjectModel;

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

        public string BlackTakenPieces
        {
            get => _blackTakenPieces;
            set
            {
                if (_blackTakenPieces != value)
                {
                    _blackTakenPieces = value;
                    OnPropertyChanged(nameof(BlackTakenPieces));
                }
            }
        }
        private string _blackTakenPieces;

        public string WhiteTakenPieces
        {
            get => _whiteTakenPieces;
            set
            {
                if (_whiteTakenPieces != value)
                {
                    _whiteTakenPieces = value;
                    OnPropertyChanged(nameof(WhiteTakenPieces));
                }
            }
        }
        private string _whiteTakenPieces;


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
                Moves = PossibleMoves(selected);
                if (Moves.Any())
                {
                    selected.Select();
                    foreach (var move in Moves)
                    {
                        Squares.First(s => s.Index == move.Index).IsPossibleMove = true;
                    }
                }
            }
            else 
            {
                var move = Moves.FirstOrDefault(_ => _.Index == index);
                if (move != null)
                {

                    if (Board.Move(Selected.Square, move))
                    {
                        string whiteTakenPieces = string.Empty;
                        string blackTakenPieces = string.Empty;
                        foreach (var piece in Board.Takens)
                        {
                            if (piece.IsWhite)
                            {
                                whiteTakenPieces += piece.ToPieceSymbol();
                            }
                            else
                            {
                                blackTakenPieces += piece.ToPieceSymbol();
                            }
                        }
                        WhiteTakenPieces = whiteTakenPieces;
                        BlackTakenPieces = blackTakenPieces;

                        var previousSelect = Selected;
                        if (move.Piece != null)
                        {
                            selected.Set(move.Piece);
                            previousSelect.Empty();
                        }

                        previousSelect.Unselect();
                        selected.Unselect();
                        Moves = new List<Square>();
                    }
                }
                Squares.ForEach(_ =>
                {
                    _.IsPossibleMove = false;
                    _.Unselect();
                });
            }
        }
        private List<Square> Moves;
    }
}
