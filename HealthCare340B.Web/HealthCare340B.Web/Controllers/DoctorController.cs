using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.ViewModel;
using HealthCare340B.DataModel;
using Microsoft.CodeAnalysis;


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
            if (medFac != null && medFac.Any())
            {
                ViewBag.MedicalFacility = medFac;
            }
            else
            {
                ViewBag.Doctor = new List<VMMDoctor>();
            }

            var spec = await specialization.GetAll();
            if (spec != null && spec.Any())
            {
                ViewBag.Specialization = spec;
            }
            else
            {
                ViewBag.Specialization = new List<VMMSpecialization>();
            }

            var treatment = await doctorTreatment.GetAll();
            if (treatment != null && treatment.Any())
            {
                ViewBag.DoctorTreatment = treatment;
            }
            else
            {
                ViewBag.DoctorTreatment = new List<VMTDoctorTreatment>();
            }
            return View();
        }

        public async Task<IActionResult> ResultSearchDoctor(string? location, string? doctorName, string? specialization, string? treatment)
        {
            List<VMMDoctor>? data = await doctor.GetByFilter(location, doctorName, specialization, treatment);
            ViewBag.Title = "Hasil Cari Dokter";
            return View();
        }


    }
}
