namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubscriptionStatus
    {
        public SubscriptionStatus()
        {
            this.UserSubscription = new HashSet<UserSubscription>();
        }
    
        public int ID { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    
        public virtual ICollection<UserSubscription> UserSubscription { get; set; }
    }
}
