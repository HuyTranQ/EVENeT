using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EVENeTWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public DataClassesDataContext Data { get; set; }

        public Service()
        {
            Data = new DataClassesDataContext(@"Data Source=(local);Initial Catalog=EVENet;Integrated Security=True");
        }

        public bool UsernameExisted(string username)
        {
            return (bool)Data.isUserExisted(username);
        }

        public void CreateUser(string username, string password, Binary profilePic, int type)
        {
            Data.createUser(username, password, profilePic, type);
        }

        public void CreateIndividual(string username, string password, Binary profilePic, string firstName, string midName, string lastName, DateTime dob, bool gender)
        {
            Data.createIndividual(username, password, profilePic, firstName, midName, lastName, dob, gender);
        }

        public bool CorrectUserNameAndPassword(string username, string password)
        {
            return (bool)Data.auth(username, password);
        }
    }
}
