using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

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

        public async Task<List<VMTAppointmentDone>> GetAll(long parentId)
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
                Console.WriteLine("AppointmentHistoryModel.GetAll: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetAll: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMTAppointmentDone>> GetByFilter(string filter, long parentId)
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
                Console.WriteLine("AppointmentHistoryModel.GetByFilter: " + e.Message);
                throw new Exception("AppointmentHistoryModel.GetByFilter: " + e.Message);
            }

            return data;

        }
    }
}
