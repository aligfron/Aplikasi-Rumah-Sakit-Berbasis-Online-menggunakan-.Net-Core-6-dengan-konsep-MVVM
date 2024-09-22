using System;
using System.Collections.Generic;

namespace HealthCare340B.DataModel
{
    public partial class MDoctorEducation
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? EducationLevelId { get; set; }
        public string? InstitutionName { get; set; }
        public string? Major { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public bool? IsLastEducation { get; set; }
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
        public virtual MEducationLevel? EducationLevel { get; set; }
        public virtual MUser? ModifiedByNavigation { get; set; }
    }
}
