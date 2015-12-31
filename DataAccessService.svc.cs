using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ExperiencePortal.DataAccess;
using ExperiencePortal.Service.Extensions;

namespace ExperiencePortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    public class DataAccessService : IDataAccessService
    {
        public Models.User GetUserById(int id)
        {
            using (DataContext context = new DataContext())
            {
                return context.GetByEntity<User>().GetItemById(id).Convert();
            }
        }

        public Models.UserPost GetPostById(int id)
        {
            using (DataContext context = new DataContext())
            {
                return context.GetByEntity<UserPost>().GetItemById(id).Convert();
            }
        }

        public Models.SubscriptionStatus GetSubscriptionStatusById(int id)
        {
            using (DataContext context = new DataContext())
            {
                return context.GetByEntity<SubscriptionStatus>().GetItemById(id).Convert();
            }
        }

        //public Models.UserSubscription.

        public Models.User AuthenticateUser(string authentivationToken, string userName)
        {
            using (DataContext dataContext = new DataAccess.DataContext())
            {
                User userData;
                userData = dataContext.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authentivationToken);
                if (userData == null)
                {
                    userData = new User()
                    {
                        UserName = userName,
                        AuthenticationToken = authentivationToken
                    };
                    dataContext.GetByEntity<User>().Add(userData);
                    dataContext.Save();
                }
                else
                {
                    userData.UserName = userName;
                }
                return userData.Convert();
            }
        }

        public Models.User[] GetAllUsers(string authenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                if (dataContext.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authenticationToken) != null)
                {
                    List<Models.User> result = new List<Models.User>();
                    var data = dataContext.GetByEntity<User>().All().Where(u => u.AuthenticationToken != authenticationToken &&
                    !u.SubscribedBy.Any(u2 => u2.User.AuthenticationToken == authenticationToken));
                    foreach (var item in data)
                    {
                        result.Add(item.Convert());
                    }
                    return result.ToArray();
                }
                else
                    return null;
            }
        }

        public Models.User[] GetAllSubscribers(string authenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                var currentUser = dataContext.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authenticationToken);
                if (currentUser != null)
                {
                    List<Models.User> result = new List<Models.User>();
                    var data = currentUser.SubscribedBy.Select(sb => sb.User);
                    foreach (var item in data)
                    {
                        result.Add(item.Convert());
                    }
                    return result.ToArray();
                }
                else
                    return null;
            }
        }

        public Models.User[] GetMySubscriptions(string authenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                var currentUser = dataContext.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authenticationToken);
                if (currentUser != null)
                {
                    List<Models.User> result = new List<Models.User>();
                    var data = currentUser.SubscribedFor.Select(sb => sb.SubscriptionUser);
                    foreach (var item in data)
                    {
                        result.Add(item.Convert());
                    }
                    return result.ToArray();
                }
                else
                    return null;
            }
        }

        public void RemoveSubscription(string userAuthenticationToken, string subscriptionAuthenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                var userId = dataContext.GetByEntity<DataAccess.User>().All().FirstOrDefault(u => u.AuthenticationToken == userAuthenticationToken).ID;
                var subscriptionId = dataContext.GetByEntity<DataAccess.User>().All().FirstOrDefault(u => u.AuthenticationToken == subscriptionAuthenticationToken).ID;

                dataContext.GetByEntity<UserSubscription>().DeleteItemById(new { subscriptionId, userId });
            }
        }

        public Models.UserSubscription SubscribeUser(string userAuthenticationToken, string subscriptionAuthenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                var userId = dataContext.GetByEntity<DataAccess.User>().All().FirstOrDefault(u => u.AuthenticationToken == userAuthenticationToken).ID;
                var subscriptionId = dataContext.GetByEntity<DataAccess.User>().All().FirstOrDefault(u => u.AuthenticationToken == subscriptionAuthenticationToken).ID;

                var subs = new DataAccess.UserSubscription() {
                    UserID = userId,
                    SubscriptionID = subscriptionId,
                    SubscriptionStatusID = (int)Models.SubscriptionStatusEnum.Subscribed,
                    LastPostReceivedDate = null
                };

                dataContext.GetByEntity<UserSubscription>().Add(subs);

                return subs.Convert();
            }
        }

        public int Sum(int first, int second)
        {
            return first + second;
        }
    }
}