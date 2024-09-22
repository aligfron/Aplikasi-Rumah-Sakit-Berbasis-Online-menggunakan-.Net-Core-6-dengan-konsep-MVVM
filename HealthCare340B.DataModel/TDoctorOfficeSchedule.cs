using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TDoctorOfficeSchedule
    {
        public TDoctorOfficeSchedule()
        {
            TAppointmentRescheduleHistories = new HashSet<TAppointmentRescheduleHistory>();
            TAppointments = new HashSet<TAppointment>();
        }

        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? MedicalFacilityScheduleId { get; set; }
        public int? Slot { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MDoctor? Doctor { get; set; }
        public virtual MMedicalFacilitySchedule? MedicalFacilitySchedule { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistories { get; set; }
        public virtual ICollection<TAppointment> TAppointments { get; set; }
    }
}
