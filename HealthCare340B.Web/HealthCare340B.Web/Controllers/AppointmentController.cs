﻿using HealthCare340B.ViewModel;
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

        public async Task<DateTime?> GetStartDate(long doctorId, long medFacId)
        {
            DateTime? startDate = await appointment.GetStartDate(doctorId, medFacId);
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
            DateTime? endDate = await appointment.GetEndDate(doctorId, medFacId);
            return endDate;           
        }

        public async Task<List<int>?> GetScheduleDayOfWeek(long medFacId, long doctorId)
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
            List<VMMMedicalFacilitySchedule>? response = await appointment.GetMedicalFacilitySchedule(medFacId, doctorId);

            List<DateTime>? emptySlotDateTime = null;
            if (response != null)
                emptySlotDateTime = await appointment.GetEmptySlotDate(response);

            List<string>? emptyHourSlot = new List<string>();
            DateTime dateChoice = DateTime.Parse(date);

            foreach (DateTime dateTime in emptySlotDateTime)
            {
                if (dateTime.Date == dateChoice.Date)
                {
                    emptyHourSlot.Add(dateTime.Hour.ToString() + ":" + dateTime.Minute.ToString());
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
                string dayString = ((DayOfWeek)day).ToString();
                return await appointment.GetDoctorOfficeScheduleId(doctorId, medFacId, dayString, timeStart);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<VMResponse<VMTAppointment>> Create(long custId, long dofId, long dosId, long dotId, string appDate)
        {
            try
            {
                // If custId = 0, find custId using biodataId session!
                VMTAppointment data = new VMTAppointment
                {
                    CustomerId = custId,
                    DoctorOfficeId = dofId,
                    DoctorOfficeScheduleId = dosId,
                    DoctorOfficeTreatmentId = dotId,
                    AppointmentDate = DateTime.Parse(appDate),
                    CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!)
                };

                return await appointment.Create(data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}
