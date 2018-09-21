using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace TenenbaumTest.Converter
{
    public class FileToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = new BitmapImage();
            SetImage(image, value as StorageFile);
            return image;
        }

        async void SetImage(BitmapImage image, StorageFile file)
        {
            if (file == null)
                return;
            //var stream = await file.GetThumbnailAsync(ThumbnailMode.ListView);
            var stream = await file.OpenReadAsync();
            await image.SetSourceAsync(stream);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
