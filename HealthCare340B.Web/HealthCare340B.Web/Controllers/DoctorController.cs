using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.ViewModel;
using HealthCare340B.DataModel;
using HealthCare340B.Web.AddOns;

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
        private ProfileModel profile;

        public DoctorController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            doctor = new DoctorModel(_config, _webHostEnv);
            medicalFacility = new MedicalFacilityModel(_config);
            specialization = new SpecializationModel(_config);
            doctorTreatment = new DoctorTreatmentModel(_config);
            imageFolder = _config["ImageFolder"];
            profile = new ProfileModel(_config, _webHostEnv);
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

            var medFac = await doctor.GetAllLocation();
            ViewBag.Location = medFac ?? new List<VMMLocation>();

            var spec = await specialization.GetAll();
            ViewBag.Specialization = spec ?? new List<VMMSpecialization>();

            var treatment = await doctorTreatment.GetAll();
            ViewBag.DoctorTreatment = treatment ?? new List<VMTDoctorTreatment>();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResultSearchDoctor(VMMDoctor dataFilter)
        {
            List<VMMDoctor>? data = await doctor.GetByFilter(
                dataFilter.LocationId?.Trim(),
                dataFilter.Fullname?.Trim(),
                dataFilter.Specialization?.Trim(),
                dataFilter.Treatment?.Trim()
            );

            ViewBag.Fullname = dataFilter.Fullname?.Trim();
            //ViewBag.MedFacName = dataFilter.MedicalFacilityName?.Trim();
            ViewBag.LocationId = dataFilter.LocationId?.Trim();
            ViewBag.Specialization = dataFilter.Specialization?.Trim();
            ViewBag.Treatment = dataFilter.Treatment?.Trim();

            if (data != null)
            {
                // Log or Debug the received data
                foreach (var doc in data)
                {
                    Console.WriteLine($"Doctor: {doc.Fullname}, Specialization: {doc.Specialization}, Location: {doc.LocationId}");
                }


                foreach (var doc in data)
                {
                    doc.IsOnline = DetermineIfDoctorIsOnline(doc);
                    doc.IsAvailable = DetermineIfDoctorIsAvailable(doc);
                }
            }
            if (dataFilter.LocationId != null)
            {
                var medFac = await doctor.GetLocationById(dataFilter?.LocationId);
                ViewBag.Location = medFac.Name;
            }

            if(dataFilter.Specialization != null)
            {
                var spec = await doctor.GetSpecializationById(dataFilter?.Specialization);
                ViewBag.SpecializationName = spec.Name;
            }
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

        private bool DetermineIfDoctorIsOnline(VMMDoctor doctor)
        {
            return doctor.MedicalFacilityCategory != null && doctor.MedicalFacilityCategory.Equals("Online", StringComparison.OrdinalIgnoreCase);
        }

        private bool DetermineIfDoctorIsAvailable(VMMDoctor doctor)
        {
            var now = DateTime.Now;
            var currentDay = now.DayOfWeek.ToString();
            var currentTime = now.TimeOfDay;

            if (doctor.MedicalFacilityScheduleDay != null && doctor.MedicalFacilityScheduleDay.Equals(currentDay, StringComparison.OrdinalIgnoreCase))
            {
                if (doctor.MedicalFacilityScheduleStartTime.HasValue && doctor.MedicalFacilityScheduleEndTime.HasValue)
                {
                    if (currentTime >= doctor.MedicalFacilityScheduleStartTime.Value && currentTime <= doctor.MedicalFacilityScheduleEndTime.Value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //punya ali

        public async Task<IActionResult> DetailDoctor(int id, bool? isOnline, bool? isAvailable)
        {
            ViewBag.Title = "Detail Dokter";
            ViewBag.imgFolder = imageFolder;
            ViewBag.Role = HttpContext.Session.GetString("userRoleCode")!;

            VMMDoctor? data = null;

            data = await profile.GetByDetailDokter(id);
            if (data == null)
            {
                return NotFound("Doctor not found for the given ID.");
            }

            // Ambil nilai isOnline dan isAvailable dari query string
            ViewBag.IsOnline = isOnline ?? false;
            ViewBag.IsAvailable = isAvailable ?? false;

            return View(data);
        }

    }
}
