using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class AppointmentModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        private HttpContent content;
        private string jsonData;

        public AppointmentModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];

        }

        public async Task<VMMDoctor?> GetDoctor(long id)
        {
            VMMDoctor? data = null;
            VMResponse<VMMDoctor>? apiResponse = new VMResponse<VMMDoctor>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}Doctor/GetById/{id}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMDoctor>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Doctor is not Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetDoctor: " + e.Message);
                throw new Exception("AppointmentModel.GetDoctor: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMMMedicalFacility>?> GetMedicalFacility(long doctorId)
        {
            List<VMMMedicalFacility>? data = null;
            VMResponse<List<VMMMedicalFacility>>? apiResponse = new VMResponse<List<VMMMedicalFacility>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                  $"{apiUrl}MedicalFacility/GetByDoctorId/{doctorId}"
                  );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMMedicalFacility>>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No Medical Facility is Found!");
                    }
                }
                else
                {
                    throw new Exception("Medical Facility API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetMedicalFacility: " + e.Message);
                throw new Exception("AppointmentModel.GetMedicalFacility: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMMMedicalFacilitySchedule>?> GetMedicalFacilitySchedule(long medicalFacilityId, long doctorId)
        {
            List<VMMMedicalFacilitySchedule>? data = null;
            VMResponse<List<VMMMedicalFacilitySchedule>>? apiResponse = new VMResponse<List<VMMMedicalFacilitySchedule>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                 $"{apiUrl}MedicalFacilitySchedule/GetByMedicalFacilityIdAndDoctorId/{medicalFacilityId}/{doctorId}"
                 );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMMedicalFacilitySchedule>>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No Medical Facility Schedule is Found!");
                    }
                }
                else
                {
                    throw new Exception("Medical Facility Schedule API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetMedicalFacilitySchedule: " + e.Message);
                throw new Exception("AppointmentModel.GetMedicalFacilitySchedule: " + e.Message);
            }

            return data;
        }

        public async Task<List<DateTime>?> GetEmptySlotDate(List<VMMMedicalFacilitySchedule> data)
        {
            List<DateTime>? apiResponse = null;
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                 JsonConvert.DeserializeObject<List<DateTime>?>(     // Convert the Json string to a class
                     await httpClient.PostAsync($"{apiUrl}Appointment/GetEmptySlotDate", content)    // Call the API
                     .Result                                                     // Read the Result
                     .Content                                                    // Get the Content Result
                     .ReadAsStringAsync()                                        // Convert the content as string
                 );
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return apiResponse;
            
        }

        public async Task<List<VMTDoctorTreatment>?> GetTreatment(long medicalFacilityId, long doctorId)
        {
            List<VMTDoctorTreatment>? data = null;
            VMResponse<List<VMTDoctorTreatment>>? apiResponse = new VMResponse<List<VMTDoctorTreatment>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                 $"{apiUrl}DoctorTreatment/GetByDoctorIdAndMedicalFacilityId/{medicalFacilityId}/{doctorId}"
                 );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMTDoctorTreatment>>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No Doctor Treatment is Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor Treatment Schedule API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetTreatment: " + e.Message);
                throw new Exception("AppointmentModel.GetTreatment: " + e.Message);
            }

            return data;
        }

        public async Task<VMResponse<VMTAppointment>?> Create(VMTAppointment data)
        {
            VMResponse<VMTAppointment>? apiResponse = new VMResponse<VMTAppointment>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                   JsonConvert.DeserializeObject<VMResponse<VMTAppointment>?>(     // Convert the Json string to a class
                       await httpClient.PostAsync($"{apiUrl}Appointment", content)    // Call the API
                       .Result                                                     // Read the Result
                       .Content                                                    // Get the Content Result
                       .ReadAsStringAsync()                                        // Convert the content as string
                   );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.Created)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Appointment API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.Create: " + e.Message);
                throw new Exception("AppointmentModel.Create: " + e.Message);
            }

            return apiResponse;
        }
    }
}
