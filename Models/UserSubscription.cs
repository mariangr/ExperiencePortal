namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserSubscription
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string SubscriptionID { get; set; }
        [DataMember]
        public DateTime? LastPostReceivedDate { get; set; }
    }
}
