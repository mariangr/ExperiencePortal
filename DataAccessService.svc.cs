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
                dataContext.GetByEntity<UserSubscription>().DeleteItemById(subscriptionAuthenticationToken, userAuthenticationToken);
            }
        }

        public Models.UserSubscription SubscribeUser(string userAuthenticationToken, string subscriptionAuthenticationToken)
        {
            using (DataContext dataContext = new DataContext())
            {
                var subs = new DataAccess.UserSubscription() {
                    UserID = userAuthenticationToken,
                    SubscriptionID = subscriptionAuthenticationToken,
                    LastPostReceivedDate = DateTime.Now
                };

                dataContext.GetByEntity<UserSubscription>().Add(subs);

                return subs.Convert();
            }
        }

        public bool MakeNewPost(string authenticationToken, Models.UserPost post)
        {
            try
            {
                using (DataContext context = new DataContext())
                {
                    context
                        .GetByEntity<User>()
                        .All()
                        .FirstOrDefault(u => u.AuthenticationToken == authenticationToken).UserPosts
                        .Add(post.Convert());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Models.UserPost[] GetMyPosts(string authenticationToken)
        {
            using (DataContext context = new DataContext())
            {
                var data = context.GetByEntity<User>().All().FirstOrDefault(u => u.AuthenticationToken == authenticationToken).UserPosts;
                List<Models.UserPost> result = new List<Models.UserPost>();

                foreach (var item in data)
                {
                    result.Add(item.Convert());
                }

                result.Sort((u, s) => (int)(s.PostDate.Value.Ticks - u.PostDate.Value.Ticks));

                return result.ToArray();
            }
        }

        public Models.UserPost[] GetSubscribedPosts(string authenticationToken)
        {
            using (DataContext context = new DataContext())
            {
                var data = context
                    .GetByEntity<User>()
                    .All()
                    .FirstOrDefault(u => u.AuthenticationToken == authenticationToken).SubscribedFor.Select(sf => sf.SubscriptionUser.UserPosts);
                List<Models.UserPost> result = new List<Models.UserPost>();

                foreach (var item in data)
                {
                    foreach(var post in item)
                        result.Add(post.Convert());
                }

                result.Sort((u,s) => (int)(s.PostDate.Value.Ticks - u.PostDate.Value.Ticks));

                return result.ToArray();
            }
        }

        public bool GetNotification(string authenticationToken)
        {
            bool result = false;

            using (DataContext context = new DataContext())
            {
                var userSubscriptions = context.GetByEntity<UserSubscription>().All().Where(s => s.UserID == authenticationToken).ToList();

                for (var i = 0; i < userSubscriptions.Count(); i++)
                {
                    if (!result)
                    {
                        DateTime lastTimeChecked = userSubscriptions[i].LastPostReceivedDate.Value;

                        List<UserPost> currentPosts = userSubscriptions[i].SubscriptionUser.UserPosts.Where(p => p.PostDate > lastTimeChecked).ToList() ;
                        if (currentPosts.Count > 0)
                        {
                            result = true;
                        }
                    }
                    userSubscriptions[i].LastPostReceivedDate = DateTime.Now;
                }
            }

            return result;
        }

        public int Sum(int first, int second)
        {
            return first + second;
        }
    }
}