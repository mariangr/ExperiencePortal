namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public partial class User
    {
        public User()
        {
            this.UserPosts = new HashSet<UserPost>();
            this.UserSubscription = new HashSet<UserSubscription>();
            this.UserSubscription1 = new HashSet<UserSubscription>();
        }
    
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string AuthenticationToken { get; set; }

        [DataMember]
        public string UserName { get; set; }
    
        public virtual ICollection<UserPost> UserPosts { get; set; }
        public virtual ICollection<UserSubscription> UserSubscription { get; set; }
        public virtual ICollection<UserSubscription> UserSubscription1 { get; set; }
    }
}
