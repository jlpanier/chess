using Chess.ViewModels;
using Repository.Dbo;

namespace Chess.Pages
{
    /// <summary>
    /// Gestion de la page principale
    /// </summary>
    public partial class MainPage : ContentPage
    {
         public MainPage()
        {
            InitializeComponent();
            AppShell.SetNavBarIsVisible(this, false);
        }

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            AppShell.SetNavBarIsVisible(this, false);
        }

        /// <summary>
        /// customize behavior immediately prior to the page becoming visible.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var times = 0;
            while (!PositionDbo.Instance.IsReady())
            {
                times++;
                await Task.Delay(100);
            }
            if (BindingContext is MainViewModel vm)
            {
                if (!vm.Board.Moves.Any()) // Evites de rafraichir la partie en cours si on revient à la page principale (ou popup)
                {
                    vm.NewGame();
                }
            }
        }
    }
}