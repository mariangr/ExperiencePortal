using ExperiencePortal.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace ExperiencePortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NotificationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NotificationService.svc or NotificationService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.PerSession)]
    public class NotificationService : INotificationService
    {
        public INotificationServiceCallback CallBack
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<INotificationServiceCallback>();
            }
        }

        public void GetNotification(string authenticationToken)
        {
            bool result = false;

            using (DataContext context = new DataContext())
            {
                var userSubscriptions = context.GetByEntity<UserSubscription>().All().Where(s => s.UserID == authenticationToken);

                foreach (var subscription in userSubscriptions)
                {
                    if (!result)
                    {
                        DateTime lastTimeChecked = subscription.LastPostReceivedDate.Value;

                        List<UserPost> currentPosts = subscription.SubscriptionUser.UserPosts.Where(sp => sp.PostDate > lastTimeChecked).ToList();
                        if (currentPosts.Count > 0)
                        {
                            result = true;
                        }
                    }
                    subscription.LastPostReceivedDate = DateTime.Now;
                }
            }

            CallBack.SendResult(result);
        }
    }
}
