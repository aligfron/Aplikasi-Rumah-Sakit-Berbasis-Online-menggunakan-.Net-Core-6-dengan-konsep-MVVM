using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MMedicalFacilitySchedule
    {
        public MMedicalFacilitySchedule()
        {
            TDoctorOfficeSchedules = new HashSet<TDoctorOfficeSchedule>();
        }

        public long Id { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string? Day { get; set; }
        public string? TimeScheduleStart { get; set; }
        public string? TimeScheduleEnd { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MMedicalFacility? MedicalFacility { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; }
    }
}
