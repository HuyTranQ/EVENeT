using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVENeT.EVENeTServiceReference;

namespace EVENeT.DataModel
{
    public class UserSearchResults
    {
        public ObservableCollection<User> users = new ObservableCollection<User>();

        public void addUser(User user)
        {
            users.Add(user);
        }

        public async Task getUsersFromName(string name)
        {
            IEnumerable<getUserFromNameResult> list = await DatabaseHelper.Client.GetUsersByNameAsync(name);
            foreach (getUserFromNameResult userlist in list)
            {
                User user = new User(userlist.username, userlist.profilePicture, userlist.Name);
                addUser(user);
            }
        }
    }
}
