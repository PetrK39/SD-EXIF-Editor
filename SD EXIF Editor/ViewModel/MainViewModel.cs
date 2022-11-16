using SD_EXIF_Editor.Model;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF.Utils;

namespace SD_EXIF_Editor.ViewModel
{
    public class MainViewModel : PropertyChangedBase
    {
        private bool image_retrieved;
        private BitmapImage bitmapImage;

        public string InputMetadata { get; set; }
        public string FilePath => image.FilePath;
        public BitmapImage BitmapImage
        {
            get
            {
                if (!image_retrieved) getImageAsync();
                return bitmapImage;
            }
        }

        private async Task getImageAsync()
        {
            image_retrieved = true;
            bitmapImage = await CreateImageAsync(FilePath).ConfigureAwait(true);
            RaisePropertyChanged(nameof(BitmapImage));
        }
        private async Task<BitmapImage> CreateImageAsync(string filename)
        {
            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                try
                {
                    byte[] buffer = await ReadAllFileAsync(filename).ConfigureAwait(false);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
                catch
                {
                    return null;
                }
            }
            else return null;
        }
        private async Task<byte[]> ReadAllFileAsync(string filename)
        {
            try
            {
                using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    byte[] buff = new byte[file.Length];
                    await file.ReadAsync(buff, 0, (int)file.Length).ConfigureAwait(false);
                    return buff;
                }
            }
            catch
            {
                return null;
            }
        }

        private readonly Image image;

        public MainViewModel(Image image)
        {
            this.image = image;
            InputMetadata = image.SDMetadata;

            ApplicationCommands.Close.InputGestures.Add(new KeyGesture(Key.Escape));
        }

        #region Commands
        public void SaveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            image.SDMetadata = InputMetadata;
            image.SaveChanges();

            ApplicationCommands.Close.Execute(null, null);
        }

        public void CopyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var cleared = InputMetadata;

            var embedIndex = InputMetadata.IndexOf("\nUsed embeddings: ");
            if (embedIndex < 0) embedIndex = InputMetadata.IndexOf("\nUsed embeddings: ");

            if (embedIndex <= 0) { } // looks good
            else cleared = cleared.Substring(0, embedIndex - 1);

            Clipboard.SetText(cleared);
        }

        public void DeleteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            InputMetadata = "";
            SaveCommandExecuted(this, null);
        }
        #endregion
    }
}
