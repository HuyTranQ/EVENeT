using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVENeT.EVENeTServiceReference;

namespace EVENeT.DataModel
{
  public class UserList
    {
        public ObservableCollection<User> users = new ObservableCollection<User>();

        public void addUser(User user)
        {
            users.Add(user);
        }

        public async Task getFollowingList(string username)
        {
            //GetIndividualFollowingRequest followingre = new GetIndividualFollowingRequest(DatabaseHelper.CurrentUser);
            //GetIndividualFollowingResponse r = await DatabaseHelper.Client.GetIndividualFollowingAsync(followingre);
            //    IEnumerable<getFollowingResult>  DatabaseHelper.C.getFollowingList(DatabaseHelper.CurrentUser);
            IEnumerable<followingListResult> list =  await DatabaseHelper.Client.GetFollowingListAsync(username);
            foreach (followingListResult userlist in list)
            {
                User user = new User(userlist.username, userlist.profilePicture, userlist.Name);
                addUser(user);
            }
        }

        public async Task getFollowerList(string username)
        {
            IEnumerable<followerListResult> list = await DatabaseHelper.Client.GetFollowerListAsync(username);
            foreach (followerListResult userlist in list)
            {
                User user = new User(userlist.username, userlist.profilePicture, userlist.Name);
                addUser(user);
            }
        }

    }
}
