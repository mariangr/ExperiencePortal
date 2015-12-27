namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class SubscriptionStatus
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public string StatusDescription { get; set; }
    }
}
