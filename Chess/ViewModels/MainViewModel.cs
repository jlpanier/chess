using Business;
using Chess.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.Mvvm;
using FFImageLoading.Helpers;
using Repository.Dbo;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

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
        /// LIste des pièces noires prises par les blancs
        /// </summary>
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

        /// <summary>
        /// Liste des pièces blanches prises par les noirs
        /// </summary>
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

        [RelayCommand]
        private async Task Download()
        {
            try
            {
                var saver = ServiceHelper.GetService<IFileSaver>();
                if (File.Exists(PositionDbo.DbPath))
                {
                    await saver.SaveToDownloadsAsync(Path.GetFileName(PositionDbo.DbPath), File.ReadAllBytes(BaseDbo.DbPath));
                    //saver.Download(PositionDbo.DbPath);
                    await ServiceHelper.GetService<IAlertService>().ShowAlertAsync("Chess", "Base de données dans Downloads", "Ok");
                }
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>().ShowAlertAsync("Chess", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private void ResetGame()
        {
            NewGame();
        }


        /// <summary>
        /// Initialisation du jeux - Squares
        /// </summary>
        public void NewGame()
        {
            var sw = new Stopwatch();
            sw.Start();
            var sb = new StringBuilder();

            Board.NewGame();
            sb.Append($"{sw.ElapsedMilliseconds} ");

            var items = new List<SquareViewModel>();
            if(Board.Squares == null) return;
            
            foreach (var square in Board.Squares)
            {
                items.Add(new SquareViewModel(this, square));
            }
            sb.Append($"{sw.ElapsedMilliseconds} ");
            Squares = new ObservableCollection<SquareViewModel>(items);
            sb.Append($"{sw.ElapsedMilliseconds} ");
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
        /// Sélection d'un case de l'échiquier
        /// </summary>
        public void Select(int index)
        {
            var selected = GetSquare(index);
            if (Selected == null) // Aucune pièce pré-sélectionnée
            {
                Board.Select(selected.Square);
            }
            else if (Selected.Index == selected.Index) // Clic sur la même pièce sélectionnée
            {
                Board.Unselect();
            }
            else
            {
                if (Board.Move(Selected.Square, selected.Square))
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
                 }
            }

            Squares.ForEach(_ => _.Hint());
        }


        /// <summary>
        /// Double clic sur une case de l'échiquier
        /// </summary>
        public void DoubleClick(int index)
        {
            Board.DoubleClick(GetSquare(index).Square);
            Squares.ForEach(_ => _.Hint());
        }
    }
}
