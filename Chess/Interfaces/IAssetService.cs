
namespace Chess.Interfaces
{
    public interface IAssetService
    {
        Task<string> EnsureAssetCopiedAsync(string assetName, string destPath);
    }

}
