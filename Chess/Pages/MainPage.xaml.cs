using Chess.ViewModels;
using System.Windows.Input;

namespace Chess.Pages
{
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

        //private void OnBoardContainerSizeChanged(object sender, EventArgs e)
        //{
        //    var size = Math.Min(BoardContainer.Width, BoardContainer.Height);
        //    BoardContainer.WidthRequest = size;
        //    BoardContainer.HeightRequest = size;
        //}

    }
}