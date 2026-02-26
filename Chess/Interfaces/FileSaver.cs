namespace Chess.Interfaces
{
    public partial class FileSaver : IFileSaver
    {
        public bool Download(string source)
        {
#if WINDOWS
            string dest = Path.Combine("C:\\Users\\jean-\\Downloads", Path.GetFileName(source));
            File.Copy(source, dest, true);
            return true;
#endif
#if ANDROID
            return Chess.ForAndroid.FileSaver.Download(source);
#endif

            throw new NotImplementedException();
        }

        Task<string?> IFileSaver.SaveToDownloadsAsync(string filename, byte[] data)
        {
#if ANDROID
            return Chess.ForAndroid.FileSaver.SaveToDownloadsAsync(filename, data);
#endif
            throw new NotImplementedException();
        }
    }

}
