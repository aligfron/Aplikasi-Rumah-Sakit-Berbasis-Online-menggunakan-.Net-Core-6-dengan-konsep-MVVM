using Newtonsoft.Json;
using System.Net;
using HealthCare340B.ViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace HealthCare340B.Web.Models
{
    public class DokterModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        HttpContent content;
        private string jsonData;

        public DokterModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<List<VMMDoctor>?> GetByFilter(VMMDoctor data)
        {
            List<VMMDoctor>? resultData = null;
            VMResponse<List<VMMDoctor>>? apiResponse = null;

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMDoctor>>?>(
                    await (await httpClient.SendAsync(new HttpRequestMessage(
                        HttpMethod.Get, $"{apiUrl}Doctor") { Content = content})).Content.ReadAsStringAsync());

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        resultData = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Dokter API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"DokterModel.GetByFilter: {e.Message}");
            }

            return resultData;
        }
    }
}