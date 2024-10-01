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
        public List<VMTDoctorOffice>? MaxEndDate { get; set; }
        public List<VMTDoctorOffice>? MinStarDate { get; set; }

        //pendidikan
        public List<VMMDoctorEducation>? InstitutionName { get; set; }

        //janji
        
        public List<VMTAppointment>? Appointment { get; set; }
        //jadwal praktek
        public List<VMMMedicalFacilitySchedule>? JadwalPraktek { get; set; }

        //harga konsul mulai 
        public List<VMTDoctorOfficeTreatmentPrice>? HargaKonsulMulai { get; set; }

        public string? LastEducationEndYear { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int ExperienceYears
        {
            get
            {
                if (LastEducationEndYear != null && int.TryParse(LastEducationEndYear, out int endYear))
                {
                    return DateTime.Now.Year - endYear;
                }
                return 0;
            }
        }

        public bool IsOnline { get; set; }
        public bool IsAvailable { get; set; }

        public string MedicalFacilityCategory { get; set; }
        public string MedicalFacilityScheduleDay { get; set; }
        public TimeSpan? MedicalFacilityScheduleStartTime { get; set; }
        public TimeSpan? MedicalFacilityScheduleEndTime { get; set; }

    }
}
