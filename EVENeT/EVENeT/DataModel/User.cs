using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVENeT.DataModel
{
  public  class User
    {
        public string Username { get; set; }
        public string Avatar { get; set; }

        public User(String Username, String Avatar)
        {
            this.Username = Username;
            this.Avatar = Avatar;
        }
    }


}
