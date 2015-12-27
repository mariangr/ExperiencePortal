namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class User
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string AuthenticationToken { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
