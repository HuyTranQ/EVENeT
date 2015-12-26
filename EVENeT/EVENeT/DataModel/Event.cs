using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace EVENeT.DataModel
{
    public class Event
    {
        public int EventId { get; set; }
        string thumbnailPath;
        public string Thumbnail
        {
            get
            {
                return thumbnailPath;
            }
            set
            {
                thumbnailPath = value;
                changeBitmap();
            }
        }
        public string Title { get; set; }

        public BitmapImage ImageThumbnail { get; set; }


        public Event(int EventId, string Title, string Thumbnail)
        {
            this.EventId = EventId;
            this.Title = Title;
            this.Thumbnail = Thumbnail;
        }

        public async void changeBitmap()
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(thumbnailPath);
            ImageThumbnail = new BitmapImage();
            await ImageThumbnail.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
        }
    }
}
