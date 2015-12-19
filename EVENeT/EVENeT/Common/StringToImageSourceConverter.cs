using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace EVENeT.Common
{
    public sealed class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string && ((string)value) != "")
            {
                Uri uri = new Uri(value as string);
                BitmapImage bmp = new BitmapImage(uri);
                return bmp;

                //var task = Task.Run(async () =>
                //{
                //    StorageFile file = await StorageFile.GetFileFromPathAsync((string)value);
                //    BitmapImage bmp = new BitmapImage();
                //    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                //    return bmp;
                //});

                //return new NotifyTaskCompletion<BitmapImage>(task);

               
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
