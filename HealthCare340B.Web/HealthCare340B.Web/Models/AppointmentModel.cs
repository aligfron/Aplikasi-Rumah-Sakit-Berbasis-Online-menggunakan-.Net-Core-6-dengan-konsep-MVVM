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

        public async Task<long?> GetDoctorOfficeId(long doctorId, long medFacId)
        {
            VMTDoctorOffice? data = null;
            VMResponse<VMTDoctorOffice>? apiResponse = new VMResponse<VMTDoctorOffice>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}DoctorOffice/GetByDoctorIdAndMedFacId/{doctorId}/{medFacId}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTDoctorOffice>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Doctor Office is not Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor Office API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetDoctorOfficeId: " + e.Message);
                throw new Exception("AppointmentModel.GetDoctorOfficeId: " + e.Message);
            }

            return data!.Id;
        }

        public async Task<long?> GetDoctorOfficeScheduleId(long doctorId, long medFacId, string day, string timeStart)
        {
            VMTDoctorOfficeSchedule? data = null;
            VMResponse<VMTDoctorOfficeSchedule>? apiResponse = new VMResponse<VMTDoctorOfficeSchedule>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}DoctorOfficeSchedule/GetByUserChoice/{doctorId}/{medFacId}/{day}/{timeStart}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTDoctorOfficeSchedule>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Doctor Office Schedule is not Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor Office Schedule API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetDoctorOfficeSchedule: " + e.Message);
                throw new Exception("AppointmentModel.GetDoctorOfficeSchedule: " + e.Message);
            }

            return data!.Id;
        }

        public async Task<DateTime?> GetStartDate(long doctorId, long medFacId)
        {
            VMTDoctorOffice? data = null;
            VMResponse<VMTDoctorOffice>? apiResponse = new VMResponse<VMTDoctorOffice>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}DoctorOffice/GetByDoctorIdAndMedFacId/{doctorId}/{medFacId}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTDoctorOffice>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Doctor Office is not Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor Office API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetStartDate: " + e.Message);
                throw new Exception("AppointmentModel.GetStartDate: " + e.Message);
            }

            return data!.StartDate;
        }

        public async Task<DateTime?> GetEndDate(long doctorId, long medFacId)
        {
            VMTDoctorOffice? data = null;
            VMResponse<VMTDoctorOffice>? apiResponse = new VMResponse<VMTDoctorOffice>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}DoctorOffice/GetByDoctorIdAndMedFacId/{doctorId}/{medFacId}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTDoctorOffice>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Doctor Office is not Found!");
                    }
                }
                else
                {
                    throw new Exception("Doctor Office API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("AppointmentModel.GetStartDate: " + e.Message);
                throw new Exception("AppointmentModel.GetStartDate: " + e.Message);
            }

            return data!.EndDate;
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

        public async Task<long> GetCustId(long bioId)
        {
            VMMCustomer? data = null;
            VMResponse<VMMCustomer>? apiResponse = new VMResponse<VMMCustomer>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}Appointment/GetCustId/{bioId}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomer>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Customer is not Found!");
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
                Console.WriteLine("AppointmentModel.GetCustId: " + e.Message);
                throw new Exception("AppointmentModel.GetCustId: " + e.Message);
            }

            return data!.Id;

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

        public async Task<List<VMTAppointment>?> GetByCustomerId(List<long> custId)
        {
            VMResponse<List<VMTAppointment>>? apiResponse = new VMResponse<List<VMTAppointment>>();

            try
            {
                jsonData = JsonConvert.SerializeObject(custId);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                 JsonConvert.DeserializeObject<VMResponse<List<VMTAppointment>>>(     // Convert the Json string to a class
                     await httpClient.PostAsync($"{apiUrl}Appointment/GetByCustomerId", content)    // Call the API
                     .Result                                                     // Read the Result
                     .Content                                                    // Get the Content Result
                     .ReadAsStringAsync()                                        // Convert the content as string
                 );

                if (apiResponse != null)
                {
                    return apiResponse.Data;
                }
                else
                {
                    return new List<VMTAppointment>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<VMTAppointmentDone>?> GetAppointmentDone(List<VMTAppointment> data)
        {
            VMResponse<List<VMTAppointmentDone>>? apiResponse = new VMResponse<List<VMTAppointmentDone>>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                 JsonConvert.DeserializeObject<VMResponse<List<VMTAppointmentDone>>>(     // Convert the Json string to a class
                     await httpClient.PostAsync($"{apiUrl}Appointment/GetAppointmentDone", content)    // Call the API
                     .Result                                                     // Read the Result
                     .Content                                                    // Get the Content Result
                     .ReadAsStringAsync()                                        // Convert the content as string
                 );

                if (apiResponse != null)
                {
                    return apiResponse.Data;
                }
                else
                {
                    return new List<VMTAppointmentDone>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<VMResponse<VMTAppointment>?> Update(VMTAppointment data)
        {
            VMResponse<VMTAppointment>? response = new VMResponse<VMTAppointment>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                response =
                   JsonConvert.DeserializeObject<VMResponse<VMTAppointment>?>(     // Convert the Json string to a class
                       await httpClient.PutAsync($"{apiUrl}Appointment", content)    // Call the API
                       .Result                                                     // Read the Result
                       .Content                                                    // Get the Content Result
                       .ReadAsStringAsync()                                        // Convert the content as string
                   );

                if (response != null)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(response.Message);
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
                Console.WriteLine("AppointmentModel.Update: " + e.Message);
                throw new Exception("AppointmentModel.Update: " + e.Message);
            }
            return response;
        }

        public async Task<VMResponse<VMTAppointment>?> DeleteOne(long id, long userId)
        {
            VMResponse<VMTAppointment>? response = new VMResponse<VMTAppointment>();
            try
            {
                response =
                   JsonConvert.DeserializeObject<VMResponse<VMTAppointment>?>(     // Convert the Json string to a class
                       await httpClient.DeleteAsync($"{apiUrl}Appointment/DeleteOne/{id}/{userId}")    // Call the API
                       .Result                                                     // Read the Result
                       .Content                                                    // Get the Content Result
                       .ReadAsStringAsync()                                        // Convert the content as string
                   );

                if (response != null)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(response.Message);
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
                Console.WriteLine("AppointmentModel.DeleteOne: " + e.Message);
                throw new Exception("AppointmentModel.DeleteOne: " + e.Message);
            }
            return response;
        }

        public async Task<VMResponse<List<VMTAppointment>>> DeleteMultiple(List<long> id, long userId)
        {
            VMResponse<List<VMTAppointment>>? response = new VMResponse<List<VMTAppointment>>();
            try
            {

                jsonData = JsonConvert.SerializeObject(id);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );
                response =
                   JsonConvert.DeserializeObject<VMResponse<List<VMTAppointment>>?>(     // Convert the Json string to a class
                       await httpClient.PutAsync($"{apiUrl}Appointment/DeleteMultiple/{userId}", content)    // Call the API
                       .Result                                                     // Read the Result
                       .Content                                                    // Get the Content Result
                       .ReadAsStringAsync()                                        // Convert the content as string
                   );

                if (response != null)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(response.Message);
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
                Console.WriteLine("AppointmentModel.DeleteMultiple: " + e.Message);
                throw new Exception("AppointmentModel.DeleteMultiple: " + e.Message);
            }
            return response;
        }
    }
}
