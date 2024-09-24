using Newtonsoft.Json;
using System.Net;
using HealthCare340B.ViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using HealthCare340B.DataModel;
using Microsoft.CodeAnalysis;

namespace HealthCare340B.Web.Models
{
    public class DoctorModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        HttpContent content;
        private string jsonData;

        public DoctorModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<List<VMMDoctor>?> GetAll()
        {
            VMResponse<List<VMMDoctor>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Doctor/GetAll");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMDoctor>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        apiResponse.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
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

            return apiResponse.Data;

        }

        public async Task<List<VMMDoctor>?> GetByFilter(string? location, string? doctorName, string? specialization, string? treatment)
        {
            List<VMMDoctor>? resultData = null;
            VMResponse<List<VMMDoctor>>? apiResponse = null;

            try
            {

                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Doctor/GetByFilter?location={location}&doctorName={doctorName}&specialization={specialization}&treatment={treatment}");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMDoctor>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        apiResponse.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
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