using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMMDoctor
    {
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
        //biodata
        public string? Fullname { get; set; }
        //spesialisasi
        public string? Specialization { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string? Treatment { get; set; }


        //tambahan ali buat profil doktor
        //biodata
        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }

        public List<VMTDoctorTreatment>? TreatmentName { get; set; }
        //tindakan medis
        public List<VMTCurrentDoctorSpecialization>? SpecializationName { get; set; }
        //Riwayat Praktek
        public List<VMTDoctorOffice>? DoctorOffice { get; set; }
        
        //pendidikan
        public List<VMMDoctorEducation>? InstitutionName { get; set; }
        //janji
        
        public List<VMTAppointment>? Appointment { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int ExperienceYears
        {
            get
            {
                var endDate = EndDate ?? DateTime.Now;
                int years = endDate.Year - StartDate.Year;
                if (endDate.Month < StartDate.Month ||
                    (endDate.Month == StartDate.Month && endDate.Day < StartDate.Day))
                {
                    years--;
                }

                return years;
            }
        }

    }
}
