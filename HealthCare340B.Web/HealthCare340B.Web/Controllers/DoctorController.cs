using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.ViewModel;
using HealthCare340B.DataModel;

namespace HealthCare340B.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorModel doctor;
        private readonly MedicalFacilityModel medicalFacility;
        private readonly SpecializationModel specialization;
        private readonly DoctorTreatmentModel doctorTreatment;

        public DoctorController(IConfiguration _config)
        {
            doctor = new DoctorModel(_config);
            medicalFacility = new MedicalFacilityModel(_config);
            specialization = new SpecializationModel(_config);
            doctorTreatment = new DoctorTreatmentModel(_config);
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> SearchDoctor()
        {
            ViewBag.Title = "Cari Dokter";

            
            var medFac = await medicalFacility.GetAll();
            ViewBag.MedicalFacility = medFac ?? new List<VMMMedicalFacility>();

            
            var spec = await specialization.GetAll();
            ViewBag.Specialization = spec ?? new List<VMMSpecialization>();

            
            var treatment = await doctorTreatment.GetAll();
            ViewBag.DoctorTreatment = treatment ?? new List<VMTDoctorTreatment>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResultSearchDoctor(VMMDoctor dataFilter)
        {
            List<VMMDoctor>? data = await doctor.GetByFilter(dataFilter.MedicalFacilityName, dataFilter.Fullname, dataFilter.Specialization, dataFilter.Treatment);

            ViewBag.Location = dataFilter.MedicalFacilityName ?? "Semua Lokasi";  
            ViewBag.Specialization = dataFilter.Specialization ?? "Semua Spesialisasi";  

            if (data == null || !data.Any())
            {
                ViewBag.Message = "Tidak ada dokter ditemukan berdasarkan pencarian Anda.";
                data = new List<VMMDoctor>(); 
            }
            
            return View("ResultSearchDoctor", data);
        }
    }
}
