using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentModel appointment;
        private CustomerMemberModel customerMember;
        private readonly string imageFolder;

        private string? userId;
        private string? roleName;

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

        private bool isInSession()
        {
            userId = HttpContext.Session.GetString("userId");

            if (userId == null)
            {
                HttpContext.Session.SetString("warnMsg", "Please Login First!");
                return false;
            }
            return true;
        }

        private bool isPasien()
        {
            roleName = HttpContext.Session.GetString("userRoleCode");

            if (roleName != "ROLE_PASIEN")
            {
                HttpContext.Session.SetString("errMsg", "You Are Not Authorized!");
                return false;
            }
            return true;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(long id)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            VMMDoctor? dataDoctor = await appointment.GetDoctor(id);
            ViewBag.Title = "Buat Janji";
            ViewBag.imgFolder = imageFolder;
            ViewBag.DoctorId = id;
            return View(dataDoctor);
        }

        public async Task<VMResponse<List<VMMCustomerMember>?>> GetMemberById(long biodataId)
        {
            VMResponse<List<VMMCustomerMember>?> response = new VMResponse<List<VMMCustomerMember>?>();
            
            if (!isInSession() || !isPasien())
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                response.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
            }

            try
            {
                response.Data = await customerMember.GetAll(biodataId);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{HttpStatusCode.OK} - Found Customer Member(s)!";
            }
            catch (Exception e)
            {
                //response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<VMResponse<List<VMMMedicalFacility>?>> GetMedFacByDoctorId(long doctorId)
        {
            VMResponse<List<VMMMedicalFacility>?> response = new VMResponse<List<VMMMedicalFacility>?>();

            if (!isInSession() || !isPasien())
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                response.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
            }

            try
            {
                response.Data = await appointment.GetMedicalFacility(doctorId);
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{HttpStatusCode.OK} - Found Medical Facility(s)!";
            }
            catch (Exception e)
            {
                //response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<DateTime?> GetStartDate(long doctorId, long medFacId)
        {
            if (!isInSession() || !isPasien())
            {
                return new DateTime();
            }

            DateTime? startDate = new DateTime();
            try
            {
                startDate = await appointment.GetStartDate(doctorId, medFacId);
            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
                throw new Exception(e.Message);
            }
            

            if (startDate > DateTime.Now)
            {
                return startDate;
            }
            else
            {
                return DateTime.Now.AddDays(1);
            }
        }
        public async Task<DateTime?> GetEndDate(long doctorId, long medFacId)
        {
            if (!isInSession() || !isPasien())
            {
                return new DateTime();
            }

            DateTime? endDate = await appointment.GetEndDate(doctorId, medFacId);
            return endDate;           
        }

        public async Task<List<int>?> GetScheduleDayOfWeek(long medFacId, long doctorId)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    throw new Exception("Unauthorized");
                }

                List<VMMMedicalFacilitySchedule>? response = await appointment.GetMedicalFacilitySchedule(medFacId, doctorId);

                List<int> listDayOfWeek = new List<int>();
                foreach (VMMMedicalFacilitySchedule day in response!)
                {
                    listDayOfWeek.Add(StringToDayOfWeekInt(day.Day!));
                }

                listDayOfWeek = listDayOfWeek.Distinct().ToList();

                return listDayOfWeek;
            }
            catch (Exception e)
            {
                return new List<int>();
            }
        }

        public async Task<List<DateTime>?> GetEmptySlotDate(long medFacId, long doctorId)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    throw new Exception("Unauthorized");
                }

                List<VMMMedicalFacilitySchedule>? response = await appointment.GetMedicalFacilitySchedule(medFacId, doctorId);

                //List<int> listDayOfWeek = new List<int>();
                //foreach (VMMMedicalFacilitySchedule day in response!)
                //{
                //    listDayOfWeek.Add(StringToDayOfWeekInt(day.Day!));
                //}

                //listDayOfWeek = listDayOfWeek.Distinct().ToList();

                List<DateTime>? emptySlotDateTime = null;
                if (response != null)
                    emptySlotDateTime = await appointment.GetEmptySlotDate(response);

                List<DateTime>? emptySlotDate = new List<DateTime>();
                if (emptySlotDateTime != null && emptySlotDateTime.Count > 0)
                {
                    int count = 0;
                    foreach (DateTime date in emptySlotDateTime)
                    {
                        foreach (VMMMedicalFacilitySchedule sch in response!)
                        {
                            DateTime timeStart = DateTime.Parse(sch.TimeScheduleStart!);
                            if (sch.Day == date.DayOfWeek.ToString() &&
                                timeStart.ToString("HH:mm") != date.ToString("HH:mm")
                                )
                            {
                                count += 1;
                            }
                        }
                        int counter = 0;
                        foreach (DateTime date2 in emptySlotDateTime)
                        {
                            if (date == date2)
                                continue;
                            if (date.Date == date2.Date)
                            {
                                counter += 1;
                            }
                        }
                        if (count == counter)
                            emptySlotDate.Add(date.Date);
                        count = 0;
                        counter = 0;
                    }
                    if (emptySlotDate.Count > 0)
                        emptySlotDate = emptySlotDate.Distinct().ToList();

                }
                return emptySlotDate;
            }
            catch (Exception e)
            {
                return new List<DateTime>();
            }
        }

        public async Task<List<VMAppointmentSchedule>> GetTimeSlot(long medFacId, long doctorId, string date)
        {
            if (!isInSession() || !isPasien())
            {
                throw new Exception("Unauthorized");
            }

            List<VMMMedicalFacilitySchedule>? response = await appointment.GetMedicalFacilitySchedule(medFacId, doctorId);

            List<DateTime>? emptySlotDateTime = new List<DateTime>();
            if (response != null)
                emptySlotDateTime = await appointment.GetEmptySlotDate(response);

            List<string>? emptyHourSlot = new List<string>();
            DateTime dateChoice = DateTime.Parse(date);

            if(emptySlotDateTime != null && emptySlotDateTime.Count > 0)
            {
                foreach (DateTime dateTime in emptySlotDateTime)
                {
                    if (dateTime.Date == dateChoice.Date)
                    {
                        emptyHourSlot.Add(dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString());
                    }
                }
            }
           
            List<VMAppointmentSchedule> listSch = new List<VMAppointmentSchedule>();

            if (emptyHourSlot.Count > 0)
            {
                foreach (string hour in emptyHourSlot)
                {
                    VMAppointmentSchedule sch = new VMAppointmentSchedule();
                    foreach (VMMMedicalFacilitySchedule mfs in response!)
                    {
                        DateTime timeStart = DateTime.Parse(mfs.TimeScheduleStart!);
                        DateTime hourParsed = DateTime.Parse(hour);
                        if (mfs.Day == dateChoice.DayOfWeek.ToString() &&
                            timeStart.ToString("HH:mm") != hourParsed.ToString("HH:mm"))
                        {
                            sch.TimeScheduleStart = mfs.TimeScheduleStart;
                            sch.Range = $"{mfs.TimeScheduleStart} - {mfs.TimeScheduleEnd}";
                            listSch.Add(sch);
                        }
                    }
                    
                }               
            }
            else
            {
                VMAppointmentSchedule sch = new VMAppointmentSchedule();
                foreach (VMMMedicalFacilitySchedule mfs in response!)
                {
                    DateTime timeStart = DateTime.Parse(mfs.TimeScheduleStart!);
                    if (mfs.Day == dateChoice.DayOfWeek.ToString())
                    {
                        sch.TimeScheduleStart = mfs.TimeScheduleStart;
                        sch.Range = $"{mfs.TimeScheduleStart} - {mfs.TimeScheduleEnd}";
                        listSch.Add(sch);
                        sch = new VMAppointmentSchedule();
                    }
                }             
            }

            return listSch;
        }

        public async Task<VMResponse<List<VMTDoctorTreatment>?>> GetTreatment(long medFacId, long doctorId)
        {
            VMResponse<List<VMTDoctorTreatment>?> response = new VMResponse<List<VMTDoctorTreatment>?>();

            if (!isInSession() || !isPasien())
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                response.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
            }

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

        public async Task<long?> GetDoctorOfficeId(long medFacId, long doctorId)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    throw new Exception("Unauthorized");
                }
                return await appointment.GetDoctorOfficeId(doctorId, medFacId);
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<long?> GetDoctorOfficeScheduleId(long doctorId, long medFacId, int day, string timeStart)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    throw new Exception("Unauthorized");
                }
                string dayString = ((DayOfWeek)day).ToString();
                return await appointment.GetDoctorOfficeScheduleId(doctorId, medFacId, dayString, timeStart);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IActionResult Check()
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Cek Ketersediaan Jadwal";
            return View();
        }

        public IActionResult EmptySlot()
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Cek Ketersediaan Jadwal";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMTAppointment>> Create(long custId, long dofId, long dosId, long dotId, string appDate)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    VMResponse<VMTAppointment> responseUnauthorized = new VMResponse<VMTAppointment>();
                    responseUnauthorized.StatusCode = HttpStatusCode.Forbidden;
                    responseUnauthorized.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
                    return responseUnauthorized;
                }

                // If custId = 0, find custId using biodataId session!
                if (custId == 0)
                    custId = await appointment.GetCustId((long)HttpContext.Session.GetInt32("userBiodataId")!);
                VMTAppointment data = new VMTAppointment
                {
                    CustomerId = custId,
                    DoctorOfficeId = dofId,
                    DoctorOfficeScheduleId = dosId,
                    DoctorOfficeTreatmentId = dotId,
                    AppointmentDate = DateTime.Parse(appDate),
                    CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!)
                };

                VMResponse<VMTAppointment> response = await appointment.Create(data);
                HttpContext.Session.SetString("successMsg", "Janji berhasil dibuat!");
                return response;
            }
            catch (Exception e)
            {
                VMResponse<VMTAppointment> response = new VMResponse<VMTAppointment>
                {
                    Message = e.Message
                };
                return response;
            }
           
        }

        public async Task<IActionResult> RencanaKedatangan(int? pageNumber, int? currPageSize, string? orderBy, string? ascDesc, string? filter)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            List<VMMCustomerMember> dataMember = new List<VMMCustomerMember>();
            try
            {
                dataMember = await customerMember.GetAll((long)HttpContext.Session.GetInt32("userBiodataId")!);
            }
            catch
            {
                dataMember = new List<VMMCustomerMember>();
            }

            long userCustId = await appointment.GetCustId((long)HttpContext.Session.GetInt32("userBiodataId")!);
            List<long> custId = new List<long>();
            custId.Add(userCustId);

            if (dataMember.Count > 0)
            {
                foreach(VMMCustomerMember member in dataMember)
                {
                    custId.Add((long)member.CustomerId!);
                }
            }

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", Controller = "Profile", Action = "Index" },
                new BreadcrumbItem { Name = "Appointment", IsActive = true }
            };


            List<VMTAppointment>? dataAppointment = await appointment.GetByCustomerId(custId);

            if (dataAppointment != null && dataAppointment.Count > 0)
            {
                foreach (VMTAppointment data in dataAppointment)
                {
                    VMMDoctor? dataDoctor = await appointment.GetDoctor((long)data.DoctorId!);
                    data.DoctorName = dataDoctor!.Fullname;
                }
            }

            if (dataAppointment == null)
                dataAppointment = new List<VMTAppointment>();
            else
            {
                List<VMTAppointmentDone>? appDone = await appointment.GetAppointmentDone(dataAppointment);

                List<VMTAppointment>? dataAppointmentCopy = new List<VMTAppointment>();
                foreach (VMTAppointment data in dataAppointment)
                {
                    dataAppointmentCopy.Add(data);
                }

                if (string.IsNullOrEmpty(filter))
                filter = "";

                if (appDone != null && appDone.Count > 0) 
                {
                    foreach (VMTAppointmentDone app in appDone)
                    {
                        foreach (VMTAppointment data in dataAppointmentCopy)
                        {
                            if (app.AppointmentId == data.Id || (!data.CustomerName!.Contains(filter) && !data.DoctorName!.Contains(filter)))
                                dataAppointment.Remove(data);
                        }
                    }
                }
                else
                {
                    foreach (VMTAppointment data in dataAppointmentCopy)
                    {
                        if (!data.CustomerName!.Contains(filter) && !data.DoctorName!.Contains(filter))
                            dataAppointment.Remove(data);
                    }
                }

                switch (orderBy)
                {
                    case "tanggalKedatangan":
                        switch (ascDesc)
                        {
                            case "asc":
                                dataAppointment = dataAppointment?.OrderBy(p => p.AppointmentDate).ToList();
                                break;
                            case "desc":
                                dataAppointment = dataAppointment?.OrderByDescending(p => p.AppointmentDate).ToList();
                                break;
                            default:
                                dataAppointment = dataAppointment?.OrderBy(p => p.AppointmentDate).ToList();
                                break;
                        }
                        break;
                    case "nama":
                        switch (ascDesc)
                        {
                            case "asc":
                                dataAppointment = dataAppointment?.OrderBy(p => p.CustomerName).ToList();
                                break;
                            case "desc":
                                dataAppointment = dataAppointment?.OrderByDescending(p => p.CustomerName).ToList();
                                break;
                            default:
                                dataAppointment = dataAppointment?.OrderBy(p => p.CustomerName).ToList();
                                break;
                        }
                        break;
                    case "tanggalDibuat":
                        switch (ascDesc)
                        {
                            case "asc":
                                dataAppointment = dataAppointment?.OrderBy(p => p.CreatedOn).ToList();
                                break;
                            case "desc":
                                dataAppointment = dataAppointment?.OrderByDescending(p => p.CreatedOn).ToList();
                                break;
                            default:
                                dataAppointment = dataAppointment?.OrderBy(p => p.CreatedOn).ToList();
                                break;
                        }
                        break;
                    default:
                        switch (ascDesc)
                        {
                            case "asc":
                                dataAppointment = dataAppointment?.OrderBy(p => p.AppointmentDate).ToList();
                                break;
                            case "desc":
                                dataAppointment = dataAppointment?.OrderByDescending(p => p.AppointmentDate).ToList();
                                break;
                            default:
                                dataAppointment = dataAppointment?.OrderBy(p => p.AppointmentDate).ToList();
                                break;
                        }
                        break;
                }
            }

            ViewBag.Filter = filter;
            ViewBag.PageSize = (currPageSize ?? 5);
            ViewBag.OrderBy = orderBy ?? "tanggalKedatangan";
            ViewBag.AscDesc = ascDesc ?? "asc";

            return View(Pagination<VMTAppointment>.Create(dataAppointment ?? new List<VMTAppointment>(), pageNumber ?? 1, ViewBag.PageSize));
        }

        public async Task<IActionResult> Update(long id, long doctorId, long custId, string custName, long medFacId, string medFacName, string appDate, string timeStart, string timeEnd, long treatmentId)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            VMMDoctor? dataDoctor = await appointment.GetDoctor(doctorId);
            ViewBag.AppointmentId = id;
            ViewBag.DoctorId = doctorId;
            ViewBag.CustomerId = custId;
            ViewBag.ImgFolder = imageFolder;
            ViewBag.CustomerName = custName;
            ViewBag.MedicalFacilityId = medFacId;
            ViewBag.MedicalFacilityName = medFacName;
            ViewBag.AppointmentDate = DateTime.Parse(appDate).ToString("yyyy-MM-dd");
            ViewBag.TimeStart = timeStart;
            ViewBag.TimeEnd = timeEnd;
            ViewBag.TreatmentId = treatmentId;
            return View(dataDoctor);
        }

        [HttpPost]
        public async Task<VMResponse<VMTAppointment>> Update(long id, long custId, long dofId, long dosId, long dotId, string appDate)
        {
            try
            {
                if (!isInSession() || !isPasien())
                {
                    VMResponse<VMTAppointment> responseUnauthorized = new VMResponse<VMTAppointment>();
                    responseUnauthorized.StatusCode = HttpStatusCode.Forbidden;
                    responseUnauthorized.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
                    return responseUnauthorized;
                }

                VMTAppointment data = new VMTAppointment
                {
                    Id = id,
                    CustomerId = custId,
                    DoctorOfficeId = dofId,
                    DoctorOfficeScheduleId = dosId,
                    DoctorOfficeTreatmentId = dotId,
                    AppointmentDate = DateTime.Parse(appDate),
                    ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!)
                };

                VMResponse<VMTAppointment>? response = await appointment.Update(data);
                HttpContext.Session.SetString("successMsg", "Janji berhasil diubah!");
                return response!;
            }
            catch (Exception e)
            {
                VMResponse<VMTAppointment> response = new VMResponse<VMTAppointment>
                {
                    Message = e.Message
                };
                return response;
            }
        }
        public IActionResult DeleteOne(long appId, string custName, string appDate, string medFacName, string docName, string treatment)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Hapus Kedatangan";
            ViewBag.AppointmentId = appId;
            ViewBag.CustomerName = custName;
            ViewBag.AppointmentDate = DateTime.Parse(appDate).ToString("dd MMMM yyyy");
            ViewBag.MedicalFacilityName = medFacName;
            ViewBag.DoctorName = docName;
            ViewBag.Treatment = treatment;
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMTAppointment>> DeleteOneAsync(long id)
        {

            if (!isInSession() || !isPasien())
            {
                VMResponse<VMTAppointment> responseUnauthorized = new VMResponse<VMTAppointment>();
                responseUnauthorized.StatusCode = HttpStatusCode.Forbidden;
                responseUnauthorized.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
                return responseUnauthorized;
            }

            VMResponse<VMTAppointment> data = new VMResponse<VMTAppointment>();

            data = await appointment.DeleteOne(id, long.Parse(HttpContext.Session.GetString("userId")!));
            HttpContext.Session.SetString("successMsg", "Janji berhasil dibatalkan!");
            return data;
        }

        [HttpPost]
        public IActionResult DeleteMultiple(List<long> id)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isPasien())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Hapus Kedatangan";
            ViewBag.AppointmentCount = id.Count;
            ViewBag.AppointmentIds = JsonConvert.SerializeObject(id);
            return View();
        }

        [HttpDelete]
        public async Task<VMResponse<List<VMTAppointment>>> DeleteMultipleAsync(List<long> id)
        {
            if (!isInSession() || !isPasien())
            {
                VMResponse<List<VMTAppointment>> responseUnauthorized = new VMResponse<List<VMTAppointment>>();
                responseUnauthorized.StatusCode = HttpStatusCode.Forbidden;
                responseUnauthorized.Message = $"{HttpStatusCode.Forbidden} - You Are Not Authorized!";
                return responseUnauthorized;
            }

            VMResponse<List<VMTAppointment>> data = new VMResponse<List<VMTAppointment>>();

            data = await appointment.DeleteMultiple(id, long.Parse(HttpContext.Session.GetString("userId")!));
            HttpContext.Session.SetString("successMsg", $"{data.Data!.Count} Janji berhasil dibatalkan!");
            return data;
        }
    }
}
