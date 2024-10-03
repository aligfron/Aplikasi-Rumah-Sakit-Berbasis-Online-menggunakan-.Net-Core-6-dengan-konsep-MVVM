using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class AppointmentHistoryModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;

        private HttpContent _content;
        private string _jsonData;

        public AppointmentHistoryModel(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<List<VMTAppointmentDone>> GetAllAppointmentDone(long parentId)
        {
            List<VMTAppointmentDone>? data = null;

            try
            {
                VMResponse<List<VMTAppointmentDone>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMTAppointmentDone>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}AppointmentDone/{parentId}")
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Appointment Done API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("AppointmentHistoryModel.GetAllAppointmentDone: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetAllAppointmentDone: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMTAppointmentDone>> GetAppointmentDoneByFilter(string filter, long parentId)
        {
            List<VMTAppointmentDone>? data = null;

            try
            {
                VMResponse<List<VMTAppointmentDone>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMTAppointmentDone>>
                >(
                    await _httpClient
                        .GetAsync(
                            (string.IsNullOrEmpty(filter))
                                ? $"{_apiUrl}AppointmentDone/{parentId}"
                                : $"{_apiUrl}AppointmentDone/GetByFilter/{parentId}/{filter}"
                        )
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Appointment Done API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("AppointmentHistoryModel.GetAppointmentDoneByFilter: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetAppointmentDoneByFilter: " + e.Message);
            }

            return data;

        }

        public async Task<VMTAppointmentDone> GetAppointmentDoneByAppointmentId(long appointmentId)
        {
            VMTAppointmentDone? data = null;

            try
            {
                VMResponse<VMTAppointmentDone>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<VMTAppointmentDone>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}AppointmentDone/GetByAppointmentId/{appointmentId}")
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Prescription API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("AppointmentHistoryModel.GetAppointmentDoneByAppointmentId: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetAppointmentDoneByAppointmentId: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMTPrescription>> GetPrescriptionByAppointmentId(long appointmentId)
        {
            List<VMTPrescription>? data = null;

            try
            {
                VMResponse<List<VMTPrescription>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMTPrescription>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}Prescription/GetByAppointmentId/{appointmentId}")
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        data = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Prescription API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("AppointmentHistoryModel.GetPrescriptionByAppointmentId: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetPrescriptionByAppointmentId: " + e.Message);
            }

            return data;
        }

        public async Task<VMResponse<List<VMTPrescription>>> UpdatePrintAttemptAsync(List<VMTPrescription> data)
        {
            VMResponse<List<VMTPrescription>>? apiResponse = new VMResponse<List<VMTPrescription>>();

            try
            {
                _jsonData = JsonConvert.SerializeObject(data);
                _content = new StringContent(_jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMTPrescription>>>(
                    await _httpClient
                        .PutAsync($"{_apiUrl}Prescription/UpdatePrintAttempt", _content)
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Prescription API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("AppointmentHistoryModel.UpdatePrintAttemptAsync: " + e.Message);
                throw new Exception("AppointmentHistoryModel.UpdatePrintAttemptAsync: " + e.Message);
            }

            return apiResponse;
        }
    }
}
