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
        void SetOrganizationInfo(string username, string name, string description, string type, string phone, string website);

        [OperationContract]
        void SetProfilePicture(string username, string imagePath);

        [OperationContract]
        void SetCoverPicture(string username, string imagePath);

        [OperationContract]
        int UserType(string username);

        [OperationContract]
        void CreateIndividual(string username, string password, string profilePic, string cover, string firstName, string midName, string lastName, DateTime dob, bool gender);

        [OperationContract]
        void CreateOrganization(string username, string password, string name, string logo, string cover, string description, string type, string phone, string website);

        [OperationContract]
        bool CreateEvent(DateTime beginTime, DateTime endTime, string description, string thumbnail, string title, int ticket, int locationId, string currentUser);

        [OperationContract]
        void CreateLocation(string name, string description, string address, double longitude, double latitude, string thumbnail);

        [OperationContract]
        int GetLocationFromAddress(string address);

        [OperationContract]
        void GetIndividual(string username, out string FirstName, out string MiddleName, out string LastName, out DateTime DOB, out bool Gender, out string ProfilePic, out string CoverPic);

        [OperationContract]
        IEnumerable<getAllEventResult> getAllEvent();

        [OperationContract]
        getEventFromIDResult GetEventFromId(int id);

        [OperationContract]
        void Follow(string username, string userToFollow);

        [OperationContract]
        bool IsFollowing(string username, string userToFollow);

        [OperationContract]
        void Unfollow(string username, string userToFollow);

        [OperationContract]
        void GetIndividualFollowing(string username, out List<string> Usernames, out List<string> DisplayNames, out List<string> ProfilePics, out List<int> Types);

        [OperationContract]
        getLocationFromIdResult GetLocationFromId(int id);
    }
}
