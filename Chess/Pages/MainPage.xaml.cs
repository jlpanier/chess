using Chess.ViewModels;
using Repository.Dbo;
using System.Diagnostics;
using System.Text;

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
            var sw = new Stopwatch();
            sw.Start();
            var sb = new StringBuilder();
            base.OnAppearing();

            if (BindingContext is MainViewModel vm)
            {
                sb.Append($"{sw.ElapsedMilliseconds} ");

                var times = 0;
                while (!PositionDbo.Instance.IsReady())
                {
                    times++;
                    await Task.Delay(100);
                }
                sb.Append($"{sw.ElapsedMilliseconds} [{times}]");
                vm.NewGame();
                sb.Append($"{sw.ElapsedMilliseconds} ");
            }
        }

    }
}