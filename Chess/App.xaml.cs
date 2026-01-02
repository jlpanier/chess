using Chess.Interfaces;
using FFImageLoading.Helpers;
using Repository.Dbo;

namespace Chess
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            var architecture = ServiceHelper.GetService<IArchitecture>();
            try
            {
                architecture.Init();
            }
            catch (FileNotFoundException)
            {
                //var asset = ServiceHelper.GetService<IAssetService>();
                //await asset.EnsureAssetCopiedAsync("CHESS.sqlite", architecture.DbPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur d'initialisation de la base de données : " + ex.Message);
                if (!ChessDbo.Instance.IsReady())
                {
                    //var asset = ServiceHelper.GetService<IAssetService>();
                    //await asset.EnsureAssetCopiedAsync("Chess.sqlite", architecture.DbPath);
                }
            }
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}