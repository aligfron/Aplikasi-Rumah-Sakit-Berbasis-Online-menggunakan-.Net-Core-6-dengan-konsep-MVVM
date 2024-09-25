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
        private readonly string imageFolder;
        private int? doctorId = null;

        public DoctorController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            doctor = new DoctorModel(_config, _webHostEnv);
            medicalFacility = new MedicalFacilityModel(_config);
            specialization = new SpecializationModel(_config);
            doctorTreatment = new DoctorTreatmentModel(_config);
            imageFolder = _config["ImageFolder"];
        }

        private bool isDoctorInSession()
        {
            doctorId = HttpContext.Session.GetInt32("doctorId");
            if (doctorId == null)
            {
                return false;
            }
            return true;
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
            List<VMMDoctor> data = await doctor.GetByFilter(
                dataFilter.MedicalFacilityName?.Trim(),
                dataFilter.Fullname?.Trim(),
                dataFilter.Specialization?.Trim(),
                dataFilter.Treatment?.Trim()
            );

            if (data != null)
            {
                // Log or Debug the received data
                foreach (var doc in data)
                {
                    Console.WriteLine($"Doctor: {doc.Fullname}, Specialization: {doc.Specialization}, Medical Facility: {doc.MedicalFacilityName}");
                }
            }

            ViewBag.Location = dataFilter.MedicalFacilityName;
            ViewBag.Specialization = dataFilter.Specialization;
            ViewBag.DoctorName = dataFilter.Fullname;
            ViewBag.Treatment = dataFilter.Treatment;
            ViewBag.ImgFolder = imageFolder;

            if (data == null || !data.Any())
            {
                ViewBag.Message = "Tidak ada dokter ditemukan berdasarkan pencarian Anda.";
                data = new List<VMMDoctor>();
            }

            return View("ResultSearchDoctor", data);
        }


    }
}
