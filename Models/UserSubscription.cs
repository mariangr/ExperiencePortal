namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserSubscription
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int SubscriptionID { get; set; }
        [DataMember]
        public DateTime? LastPostReceivedDate { get; set; }
        [DataMember]
        public int SubscriptionStatusID { get; set; }
    }
}
