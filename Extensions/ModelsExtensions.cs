using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperiencePortal.Service.Extensions
{
    public static class ModelsExtensions
    {
        public static DataAccess.User ToModel(this Models.User user)
        {
            return new DataAccess.User();
        }

        public static DataAccess.UserSubscription ToModel(this Models.UserSubscription userSubscription)
        {
            return new DataAccess.UserSubscription();
        }

        public static DataAccess.UserPost ToModel(this Models.UserPost userPost)
        {
            return new DataAccess.UserPost();
        }

        public static Models.User ToModel(this DataAccess.User user)
        {
            return new Models.User();
        }

        public static Models.UserSubscription ToModel(this DataAccess.UserSubscription userSubscription)
        {
            return new Models.UserSubscription();
        }

        public static Models.UserPost ToModel(this DataAccess.UserPost userPost)
        {
            return new Models.UserPost();
        }
    }
}