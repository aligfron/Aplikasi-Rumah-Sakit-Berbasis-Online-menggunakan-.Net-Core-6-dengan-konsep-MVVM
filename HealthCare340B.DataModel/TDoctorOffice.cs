using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class TDoctorOffice
    {
        public TDoctorOffice()
        {
            TAppointments = new HashSet<TAppointment>();
            TDoctorOfficeTreatments = new HashSet<TDoctorOfficeTreatment>();
        }

        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string Specialization { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long ServiceUnitId { get; set; }
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
        public virtual MMedicalFacility? MedicalFacility { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual MServiceUnit ServiceUnit { get; set; } = null!;
        public virtual ICollection<TAppointment> TAppointments { get; set; }
        public virtual ICollection<TDoctorOfficeTreatment> TDoctorOfficeTreatments { get; set; }
    }
}
