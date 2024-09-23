using Newtonsoft.Json;
using System.Net;
using HealthCare340B.ViewModel;

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


        public async Task<List<VMMDoctor>?> GetByName(string nama)
        {
            List<VMMDoctor>? data = null;

            try
            {
                VMResponse<List<VMMDoctor>>? apiResponse =
                JsonConvert.DeserializeObject<VMResponse<List<VMMDoctor>>?>(
                await httpClient.GetStringAsync(
                (string.IsNullOrEmpty(nama))
                ? $"{apiUrl}Category"
                : $"{apiUrl}Category/GetBy/{nama}"
                    )
                );

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = apiResponse.data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Dokter API could not be reached!");
                }
            }
            catch(Exception e)
            {
                throw new Exception($"DokterModel.GetByFilter: {e.Message}");
            }
            return data;
        }
    }
}
