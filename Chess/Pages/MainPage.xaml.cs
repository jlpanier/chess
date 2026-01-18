using Chess.ViewModels;

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

            if (BindingContext is MainViewModel vm)
            {
                vm.NewGame();
            }
        }

    }
}