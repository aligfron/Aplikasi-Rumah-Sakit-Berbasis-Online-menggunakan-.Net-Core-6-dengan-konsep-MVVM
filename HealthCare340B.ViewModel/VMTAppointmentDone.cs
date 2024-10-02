using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMTAppointmentDone
    {
        public long Id { get; set; }
        public long? AppointmentId { get; set; }

        public long? CustomerId { get; set; }
        public string? CustomerFullname { get; set; }
        public int? CustomerAge { get; set; }
        public string? CustomerGender { get; set; }

        public DateTime? AppointmentDate { get; set; }
        public long? DoctorOfficeId { get; set; }
        public string? MedicalFacilityName { get; set; }

        public string? DoctorFullname { get; set; }
        public string? SpecializationName { get; set; }
        public string? DoctorTreatmentName { get; set; }

        public string Diagnosis { get; set; } = null!;

        public List<VMTPrescription>? Prescriptions { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}
