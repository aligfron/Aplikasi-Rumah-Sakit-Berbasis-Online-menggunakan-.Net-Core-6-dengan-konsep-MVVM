using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{
    public class BiodataModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;

        private HttpContent _content;
        private string _jsonData;

        public BiodataModel(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<VMMBiodatum> GetById(long id)
        {
            VMMBiodatum? data = null;

            try
            {
                VMResponse<VMMBiodatum>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<VMMBiodatum>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}Biodata/{id}")
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
                    throw new Exception("Biodata API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("BiodataModel.GetById: " + e.Message);
                throw new Exception("BiodataModel.GetById: " + e.Message);
            }

            return data;
        }
    }
}
