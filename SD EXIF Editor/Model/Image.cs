using ExifLibrary;
using System.Linq;

namespace SD_EXIF_Editor.Model
{
    public class Image
    {
        public string FilePath { get; }
        public string SDMetadata
        {
            get => GetMetadataProperty().Value;
            set => GetMetadataProperty().Value = value;
        }

        private const string MetadataFieldName = "parameters";
        private readonly ImageFile imageFile;


        public Image(string filePath)
        {
            FilePath = filePath;
            imageFile = ImageFile.FromFile(filePath);
        }
        public void SaveChanges()
        {
            imageFile.Save(FilePath);
        }
        private PNGText GetMetadataProperty()
        {
            var prop = imageFile.Properties.Where(p => p is PNGText).Cast<PNGText>().SingleOrDefault(p => p.Keyword == MetadataFieldName);

            if (prop is null)
            {
                prop = new PNGText(ExifTag.PNGText, MetadataFieldName, "", false);
                imageFile.Properties.Add(prop);
            }

            return prop;
        }
    }
}
