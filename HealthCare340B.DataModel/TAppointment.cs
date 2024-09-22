using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TAppointment
    {
        public TAppointment()
        {
            TAppointmentCancellations = new HashSet<TAppointmentCancellation>();
            TAppointmentDones = new HashSet<TAppointmentDone>();
            TAppointmentRescheduleHistories = new HashSet<TAppointmentRescheduleHistory>();
            TPrescriptions = new HashSet<TPrescription>();
        }

        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? DoctorOfficeId { get; set; }
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

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MCustomer? Customer { get; set; }
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual TDoctorOffice? DoctorOffice { get; set; }
        public virtual TDoctorOfficeSchedule? DoctorOfficeSchedule { get; set; }
        public virtual TDoctorOfficeTreatment? DoctorOfficeTreatment { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TAppointmentCancellation> TAppointmentCancellations { get; set; }
        public virtual ICollection<TAppointmentDone> TAppointmentDones { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistories { get; set; }
        public virtual ICollection<TPrescription> TPrescriptions { get; set; }
    }
}
