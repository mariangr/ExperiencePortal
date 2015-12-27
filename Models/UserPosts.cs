namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class UserPost
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public DateTime? PostDate { get; set; }
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public byte[] Photo { get; set; }
        [DataMember]
        public double? Latitude { get; set; }
        [DataMember]
        public double? Longitude { get; set; }
    }
}
