namespace ExperiencePortal.Service.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPost
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
    
        public virtual User Users { get; set; }
    }
}
