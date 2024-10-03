using Newtonsoft.Json;
using System.Net;
using HealthCare340B.ViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using HealthCare340B.DataModel;
using Microsoft.CodeAnalysis;

namespace HealthCare340B.Web.Models
{
    public class MedicalItemModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private readonly IWebHostEnvironment webHostEnv;
        private readonly string imageFolder;


        public MedicalItemModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            webHostEnv = _webHostEnv;
            imageFolder = _config["ImageFolder"];
        }

        public async Task<List<VMMMedicalItemCategory>?> GetAllCategory()
        {
            VMResponse<List<VMMMedicalItemCategory>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}MedicalItem/GetAllCategory");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMMedicalItemCategory>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Medical Item API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"MedicalItemModel.GetAllCategory: {e.Message}");
            }
        }

        public async Task<List<VMMMedicalItemSegmentation>?> GetAllSegementation()
        {
            VMResponse<List<VMMMedicalItemSegmentation>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}MedicalItem/GetAllSegmentation");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMMedicalItemSegmentation>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Medical Item API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"MedicalItemModel.GetAllSegmentation: {e.Message}");
            }
        }

        public async Task<List<VMMMedicalItem>?> GetByFilter(long? categoryId, bool? isSegmentation, long? priceMax, long? priceMin, string? name, string? indication)
        {
            List<VMMMedicalItem>? resultData = null;
            VMResponse<List<VMMMedicalItem>>? apiResponse = new VMResponse<List<VMMMedicalItem>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}MedicalItem/GetByFilter?categoryId={categoryId}&isSegmentation={isSegmentation}&priceMax={priceMax}&priceMin={priceMin}&name={name}&indication={indication}");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        string responseContent = await apiResponseMsg.Content.ReadAsStringAsync();
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMMedicalItem>>?>(responseContent);

                        Console.WriteLine($"API Response: {responseContent}");
                    }
                    else
                    {
                        apiResponse.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    throw new Exception("Medical Item API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"MedicalItemModel.GetByFilter: {e.Message}");
            }

            return apiResponse?.Data ?? resultData;
        }

        public async Task<VMMMedicalItemCategory?> GetById(long id)
        {
            VMMMedicalItemCategory? data = null;
            VMResponse<VMMMedicalItemCategory>? apiResponse = new VMResponse<VMMMedicalItemCategory>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                    $"{apiUrl}MedicalItem/GetById/{id}"
                    );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        string responseContent = await apiResponseMsg.Content.ReadAsStringAsync();
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMMedicalItemCategory>?>(responseContent);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - Medical Item Category not found");
                    }
                }
                else
                {
                    throw new Exception("Medical Item API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("MedicalItemModel.GetById: " + e.Message);
                throw new Exception("MedicalItemModel.GetById: " + e.Message);
            }

            return data;
        }

    }
}
