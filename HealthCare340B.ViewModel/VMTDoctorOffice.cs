using HealthCare340B.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.ViewModel
{
    public class VMTDoctorOffice
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public string? LocationName { get; set; }
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
        public List<VMMMedicalFacilitySchedule>? JadwalPraktek { get; set; }
        public string? FullAddress { get; set; }
        public string? Servicename { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string? MedicalFacilityCategory { get; set; }
        
        public decimal? HargaKonsulMulai { get; set; }
        public VMTDoctorOffice() { }
        public VMTDoctorOffice(TDoctorOffice doctorOffice, MMedicalFacility mMedicalFacility, MLocation mLocation )
        {
            Id = doctorOffice.Id;
            DoctorId = doctorOffice.DoctorId;
            MedicalFacilityId = doctorOffice.MedicalFacilityId;
            LocationName = mLocation.Name;
            Specialization = doctorOffice.Specialization;
            StartDate = doctorOffice.StartDate;
            EndDate = doctorOffice.EndDate;
            ServiceUnitId = doctorOffice.ServiceUnitId;
            CreatedBy = doctorOffice.CreatedBy;
            CreatedOn = doctorOffice.CreatedOn;
            ModifiedBy = doctorOffice.ModifiedBy;
            ModifiedOn = doctorOffice.ModifiedOn;
            DeletedBy = doctorOffice.DeletedBy;
            DeletedOn = doctorOffice.DeletedOn;
            IsDelete = doctorOffice.IsDelete;
            
        }
    }
}
