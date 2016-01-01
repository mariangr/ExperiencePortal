using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperiencePortal.Service.Extensions
{
    public static class ModelsExtensions
    {
        public static DataAccess.User Convert(this Models.User user)
        {
            return new DataAccess.User()
            {
                AuthenticationToken = user.AuthenticationToken,
                ID = user.ID,
                UserName = user.UserName
            };
        }

        public static DataAccess.UserSubscription Convert(this Models.UserSubscription userSubscription)
        {
            return new DataAccess.UserSubscription()
            {
                UserID = userSubscription.UserID,
                SubscriptionID = userSubscription.SubscriptionID,
                SubscriptionStatusID = userSubscription.SubscriptionStatusID,
                LastPostReceivedDate = userSubscription.LastPostReceivedDate,
            };
        }

        public static DataAccess.UserPost Convert(this Models.UserPost userPost)
        {
            return new DataAccess.UserPost()
            {
                ID = userPost.ID,
                UserID = userPost.UserID,
                PostDate = userPost.PostDate,
                Latitude = userPost.Latitude,
                Longitude = userPost.Longitude,
                Photo = userPost.Photo,
            };
        }

        public static Models.User Convert(this DataAccess.User user)
        {
            return new Models.User()
            {
                AuthenticationToken = user.AuthenticationToken,
                ID = user.ID,
                UserName = user.UserName
            };
        }

        public static Models.UserSubscription Convert(this DataAccess.UserSubscription userSubscription)
        {
            return new Models.UserSubscription()
            {
                UserID = userSubscription.UserID,
                LastPostReceivedDate = userSubscription.LastPostReceivedDate,
                SubscriptionID = userSubscription.SubscriptionID,
                SubscriptionStatusID = userSubscription.SubscriptionStatusID
            };
        }

        public static Models.UserPost Convert(this DataAccess.UserPost userPost)
        {
            return new Models.UserPost()
            {
                ID = userPost.ID,
                UserID = userPost.UserID,
                Latitude = userPost.Latitude,
                Longitude = userPost.Longitude,
                Photo = userPost.Photo,
                PostDate = userPost.PostDate,
                User = userPost.User.Convert()
            };
        }

        public static Models.SubscriptionStatus Convert(this DataAccess.SubscriptionStatus subsStatus)
        {
            return new Models.SubscriptionStatus()
            {
                ID = subsStatus.ID,
                StatusDescription = subsStatus.StatusDescription,
                StatusName = subsStatus.StatusName
            };
        }
    }
}