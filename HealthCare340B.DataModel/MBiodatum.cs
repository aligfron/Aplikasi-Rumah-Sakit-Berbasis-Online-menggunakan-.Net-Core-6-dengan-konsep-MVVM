using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MBiodatum
    {
        public MBiodatum()
        {
            MAdmins = new HashSet<MAdmin>();
            MBiodataAddresses = new HashSet<MBiodataAddress>();
            MBiodataAttachments = new HashSet<MBiodataAttachment>();
            MCustomerMembers = new HashSet<MCustomerMember>();
            MCustomers = new HashSet<MCustomer>();
            MDoctors = new HashSet<MDoctor>();
            MUsers = new HashSet<MUser>();
        }

        public long Id { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<MAdmin> MAdmins { get; set; }
        public virtual ICollection<MBiodataAddress> MBiodataAddresses { get; set; }
        public virtual ICollection<MBiodataAttachment> MBiodataAttachments { get; set; }
        public virtual ICollection<MCustomerMember> MCustomerMembers { get; set; }
        public virtual ICollection<MCustomer> MCustomers { get; set; }
        public virtual ICollection<MDoctor> MDoctors { get; set; }
        public virtual ICollection<MUser> MUsers { get; set; }
    }
}
