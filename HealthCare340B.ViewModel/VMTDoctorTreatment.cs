using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMTDoctorTreatment
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? Name { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }

        public VMTDoctorTreatment() { }

        public VMTDoctorTreatment(TDoctorTreatment doctorTreatment, MDoctor mDoctor, MBiodatum mBiodatum)
        {
            Id = doctorTreatment.Id;
            DoctorId = doctorTreatment.DoctorId;
            DoctorName = mBiodatum.Fullname;
            Name = doctorTreatment.Name;
            CreatedBy = doctorTreatment.CreatedBy;
            CreatedOn = doctorTreatment.CreatedOn;
            ModifiedBy = doctorTreatment.ModifiedBy;
            ModifiedOn = doctorTreatment.ModifiedOn;
            DeletedBy = doctorTreatment.DeletedBy;
            DeletedOn = doctorTreatment.DeletedOn;
            IsDelete = doctorTreatment.IsDelete;
        }
    }
}
