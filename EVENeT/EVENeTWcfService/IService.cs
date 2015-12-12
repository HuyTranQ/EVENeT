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
        bool UsernameExists(string username);

        [OperationContract]
        bool CorrectUserNameAndPassword(string username, string password);

        [OperationContract]
        bool IndividualFullySetUp(string username);

        [OperationContract]
        void SetIndividualInfo(string username, string firstName, string midName, string lastName, DateTime dob, bool gender);

        [OperationContract]
        void SetProfilePicture(string username, string imagePath);

        [OperationContract]
        void SetCoverPicture(string username, string imagePath);

        [OperationContract]
        int UserType(string username);

        [OperationContract]
        void CreateIndividual(string username, string password, string profilePic, string cover, string firstName, string midName, string lastName, DateTime dob, bool gender);

        [OperationContract]
        void CreateOrganization(string username, string password, string logo, string cover, string description, string type, string phone, string website);
    }
}
