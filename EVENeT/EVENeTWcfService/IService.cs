using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EVENeTWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        bool UsernameExisted(string username);

        [OperationContract]
        bool CorrectUserNameAndPassword(string username, string password);

        [OperationContract]
        void CreateUser(string username, string password, Binary profilePic, int type);

        [OperationContract]
        void CreateIndividual(string username, string password, Binary profilePic, string firstName, string midName, string lastName, DateTime dob, bool gender);
    }
}
