using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{
    public class SpecializationModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        HttpContent content;
        private string jsonData;

        public SpecializationModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<List<VMMSpecialization>?> GetAll()
        {
            VMResponse<List<VMMSpecialization>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Specialization/GetAll");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMSpecialization>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        apiResponse!.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    throw new Exception("Medical Facility API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"MedicalFacility.GetAll: {e.Message}");
            }

            return apiResponse!.Data;

        }
    }
}
