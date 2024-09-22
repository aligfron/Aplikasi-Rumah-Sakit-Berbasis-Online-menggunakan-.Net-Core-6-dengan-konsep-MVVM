using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TDoctorOfficeTreatment
    {
        public TDoctorOfficeTreatment()
        {
            TAppointmentRescheduleHistories = new HashSet<TAppointmentRescheduleHistory>();
            TAppointments = new HashSet<TAppointment>();
            TDoctorOfficeTreatmentPrices = new HashSet<TDoctorOfficeTreatmentPrice>();
        }

        public long Id { get; set; }
        public long? DoctorTreatmentId { get; set; }
        public long? DoctorOfficeId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual TDoctorOffice? DoctorOffice { get; set; }
        public virtual TDoctorTreatment? DoctorTreatment { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<TAppointmentRescheduleHistory> TAppointmentRescheduleHistories { get; set; }
        public virtual ICollection<TAppointment> TAppointments { get; set; }
        public virtual ICollection<TDoctorOfficeTreatmentPrice> TDoctorOfficeTreatmentPrices { get; set; }
    }
}
