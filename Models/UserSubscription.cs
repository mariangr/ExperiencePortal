namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSubscription
    {
        public int UserID { get; set; }
        public int SubscriptionID { get; set; }
        public DateTime? LastPostReceivedDate { get; set; }
        public int SubscriptionStatusID { get; set; }
    
        public virtual SubscriptionStatus SubscriptionStatus { get; set; }
        public virtual User User { get; set; }
        public virtual User Subscriptions { get; set; }
    }
}
