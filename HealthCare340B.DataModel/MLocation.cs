using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MLocation
    {
        public MLocation()
        {
            InverseParent = new HashSet<MLocation>();
            MBiodataAddresses = new HashSet<MBiodataAddress>();
            MMedicalFacilities = new HashSet<MMedicalFacility>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long? ParentId { get; set; }
        public long? LocationLevelId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MLocationLevel? LocationLevel { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual MLocation? Parent { get; set; }
        public virtual ICollection<MLocation> InverseParent { get; set; }
        public virtual ICollection<MBiodataAddress> MBiodataAddresses { get; set; }
        public virtual ICollection<MMedicalFacility> MMedicalFacilities { get; set; }
    }
}
