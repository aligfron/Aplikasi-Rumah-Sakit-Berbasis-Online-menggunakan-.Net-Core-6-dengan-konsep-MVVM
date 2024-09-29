using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentModel appointment;
        private CustomerMemberModel customerMember;
        private readonly string imageFolder;

        public AppointmentController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            appointment = new AppointmentModel(_config);
            customerMember = new CustomerMemberModel(_config);
            imageFolder = _config["ImageFolder"];
        }

        private int StringToDayOfWeekInt(string dayName)
        {
            switch (dayName)
            {
                case "Sunday":
                    return 0;
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                default:
                    return -1;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(long id)
        {
            VMMDoctor? dataDoctor = await appointment.GetDoctor(id);
            ViewBag.Title = "Buat Janji";
            ViewBag.imgFolder = imageFolder;
            ViewBag.DoctorId = id;
            return View(dataDoctor);
        }

        public async Task<VMResponse<List<VMMCustomerMember>?>> GetMemberById(long biodataId)
        {
            VMResponse<List<VMMCustomerMember>?> response = new VMResponse<List<VMMCustomerMember>?>();            
            try
            {
                response.Data = await customerMember.GetAll(biodataId);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{HttpStatusCode.OK} - Found Customer Member(s)!";
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Customer Member Not Found!";
            }
            return response;
        }

        public async Task<VMResponse<List<VMMMedicalFacility>?>> GetMedFacByDoctorId(long doctorId)
        {
            VMResponse<List<VMMMedicalFacility>?> response = new VMResponse<List<VMMMedicalFacility>?>();
            try
            {
                response.Data = await appointment.GetMedicalFacility(doctorId);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{HttpStatusCode.OK} - Found Medical Facility(s)!";
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Medical Facility Not Found!";
            }
            return response;
        }

        public async Task<List<int>?> GetSchedule(long medFacId, long doctorId)
        {
            try
            {
                List<VMMMedicalFacilitySchedule>? response = await appointment.GetMedicalFacilitySchedule(medFacId, doctorId);

                List<int> listDayOfWeek = new List<int>();
                foreach (VMMMedicalFacilitySchedule day in response!)
                {
                    listDayOfWeek.Add(StringToDayOfWeekInt(day.Day!));
                }

                listDayOfWeek = listDayOfWeek.Distinct().ToList();

                List<DateTime>? emptySlotDate = null;
                if (response != null)
                    emptySlotDate = await appointment.GetEmptySlotDate(response);
                return listDayOfWeek;
            }
            catch (Exception e)
            {
                return new List<int>();
            }
        }

        public async Task<VMResponse<List<VMTDoctorTreatment>?>> GetTreatment(long medFacId, long doctorId)
        {
            VMResponse<List<VMTDoctorTreatment>?> response = new VMResponse<List<VMTDoctorTreatment>?>();
            try
            {
                response.Data = await appointment.GetTreatment(medFacId, doctorId);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{HttpStatusCode.OK} - Found Treatment(s)!";
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Treatment Not Found!";
            }
            return response;
        }
    }
}
