using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace EVENeT.DataModel
{
  public class User
    {
        public string Username { get; set; }
        string avatar;
        public string Avatar {
            get {
                return avatar;
                }
            set { avatar = value;
                changeBitmap();
                }
        }
        public string Name { get; set; }

        public BitmapImage ImageAvatar { get; set; } 


        public User(string Username, string Avatar, string Name)
        {
            this.Username = Username;
            this.Avatar = Avatar;
            this.Name = Name;
        }

        public async void changeBitmap()
        {
           StorageFile file = await StorageFile.GetFileFromPathAsync(avatar);
           ImageAvatar = new BitmapImage();
            await ImageAvatar.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
        }
    }
}
