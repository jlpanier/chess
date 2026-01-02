namespace Chess.Interfaces
{
    public class StartupService : IStartupService
    {
        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }
    }
}
