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
        //tindakan medis
        public List<VMTDoctorTreatment>? TreatmentName { get; set; }
        //Riwayat Praktek
        public List<VMTDoctorOffice>? DoctorOffice { get; set; }
        
        //pendidikan
        public List<VMMDoctorEducation>? InstitutionName { get; set; }


    }
}
