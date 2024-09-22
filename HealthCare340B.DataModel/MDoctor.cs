using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MDoctor
    {
        public MDoctor()
        {
            MDoctorEducations = new HashSet<MDoctorEducation>();
            TCurrentDoctorSpecializations = new HashSet<TCurrentDoctorSpecialization>();
            TCustomerChats = new HashSet<TCustomerChat>();
            TDoctorOfficeSchedules = new HashSet<TDoctorOfficeSchedule>();
            TDoctorOffices = new HashSet<TDoctorOffice>();
            TDoctorTreatments = new HashSet<TDoctorTreatment>();
        }

        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string? Str { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public virtual MBiodatum? Biodata { get; set; }
        public virtual MUser CreatedByNavigation { get; set; } = null!;
        public virtual MUser? DeletedByNavigation { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
        public virtual ICollection<MDoctorEducation> MDoctorEducations { get; set; }
        public virtual ICollection<TCurrentDoctorSpecialization> TCurrentDoctorSpecializations { get; set; }
        public virtual ICollection<TCustomerChat> TCustomerChats { get; set; }
        public virtual ICollection<TDoctorOfficeSchedule> TDoctorOfficeSchedules { get; set; }
        public virtual ICollection<TDoctorOffice> TDoctorOffices { get; set; }
        public virtual ICollection<TDoctorTreatment> TDoctorTreatments { get; set; }
    }
}
