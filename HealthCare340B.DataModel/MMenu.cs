using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MMenu
    {
        public MMenu()
        {
            InverseParent = new HashSet<MMenu>();
            MMenuRoles = new HashSet<MMenuRole>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public long? ParentId { get; set; }
        public string? BigIcon { get; set; }
        public string? SmallIcon { get; set; }
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
        public virtual MMenu? Parent { get; set; }
        public virtual ICollection<MMenu> InverseParent { get; set; }
        public virtual ICollection<MMenuRole> MMenuRoles { get; set; }
    }
}
