using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMTCurrentDoctorSpecialization
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? SpecializationId { get; set; }
        public string? SpecializationName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public VMTCurrentDoctorSpecialization() { }
        public VMTCurrentDoctorSpecialization(TCurrentDoctorSpecialization currentDoctorSpecialization, MSpecialization mSpecialization)
        {
            
            Id = currentDoctorSpecialization.Id;
            DoctorId = currentDoctorSpecialization.DoctorId;
            SpecializationId = currentDoctorSpecialization.SpecializationId;
            SpecializationName = mSpecialization.Name;
            CreatedBy = currentDoctorSpecialization.CreatedBy;
            CreatedOn = currentDoctorSpecialization.CreatedOn;
            ModifiedBy = currentDoctorSpecialization.ModifiedBy;
            ModifiedOn = currentDoctorSpecialization.ModifiedOn;
            DeletedBy = currentDoctorSpecialization.DeletedBy;
            DeletedOn = currentDoctorSpecialization.DeletedOn;
            IsDelete = currentDoctorSpecialization.IsDelete;

        }
    }
}
