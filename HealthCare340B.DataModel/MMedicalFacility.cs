using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MMedicalFacility
    {
        public MMedicalFacility()
        {
            MMedicalFacilitySchedules = new HashSet<MMedicalFacilitySchedule>();
            TDoctorOffices = new HashSet<TDoctorOffice>();
            TMedicalItemPurchaseDetails = new HashSet<TMedicalItemPurchaseDetail>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long? MedicalFacilityCategoryId { get; set; }
        public long? LocationId { get; set; }
        public string? FullAddress { get; set; }
        public string? Email { get; set; }
        public string? PhoneCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MLocation? Location { get; set; }
        public virtual MMedicalFacilityCategory? MedicalFacilityCategory { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<MMedicalFacilitySchedule> MMedicalFacilitySchedules { get; set; }
        public virtual ICollection<TDoctorOffice> TDoctorOffices { get; set; }
        public virtual ICollection<TMedicalItemPurchaseDetail> TMedicalItemPurchaseDetails { get; set; }
    }
}
