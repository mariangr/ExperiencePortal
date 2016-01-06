using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ExperiencePortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDataAccessService
    {
        [OperationContract]
        Models.User AuthenticateUser(string authentivationToken, string userName);

        [OperationContract]
        Models.User[] GetAllUsers(string authenticationToken);

        [OperationContract]
        Models.User[] GetAllSubscribers(string authenticationToken);

        [OperationContract]
        Models.User[] GetMySubscriptions(string authenticationToken);

        [OperationContract]
        void RemoveSubscription(string userAuthenticationToken, string subscriptionAuthenticationToken);

        [OperationContract]
        Models.UserSubscription SubscribeUser(string userAuthenticationToken, string subscriptionAuthenticationToken);

        [OperationContract]
        bool MakeNewPost(string authenticationToken, Models.UserPost post);

        [OperationContract]
        Models.UserPost[] GetMyPosts(string authenticationToken);

        [OperationContract]
        Models.UserPost[] GetSubscribedPosts(string authenticationToken);

        [OperationContract]
        bool GetNotification(string authenticationToken);

        [OperationContract]
        int Sum(int first, int second);
    }
}
