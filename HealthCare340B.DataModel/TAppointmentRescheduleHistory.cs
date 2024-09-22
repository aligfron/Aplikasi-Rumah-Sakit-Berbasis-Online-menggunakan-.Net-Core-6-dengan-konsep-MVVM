using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TAppointmentRescheduleHistory
    {
        public long Id { get; set; }
        public long? AppointmentId { get; set; }
        public long? DoctorOfficeScheduleId { get; set; }
        public long? DoctorOfficeTreatmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual TAppointment? Appointment { get; set; }
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual TDoctorOfficeSchedule? DoctorOfficeSchedule { get; set; }
        public virtual TDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
