using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{
    public class BloodGroupModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;

        private HttpContent _content;
        private string _jsonData;

        public BloodGroupModel(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<List<VMMBloodGroup>> GetAll()
        {
            List<VMMBloodGroup>? data = null;

            try
            {
                VMResponse<List<VMMBloodGroup>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMBloodGroup>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}BloodGroup")
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
                    throw new Exception("Blood Group API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("BloodGroupModel.GetAll: " + e.Message);
                throw new Exception("BloodGroupModel.GetAll: " + e.Message);
            }

            return data;
        }
    }
}
