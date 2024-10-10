using Newtonsoft.Json;
using System.Net;
using HealthCare340B.ViewModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using HealthCare340B.DataModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HealthCare340B.Web.Models
{
    public class DoctorModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private readonly IWebHostEnvironment webHostEnv;
        private readonly string imageFolder;


        public DoctorModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            webHostEnv = _webHostEnv;
            imageFolder = _config["ImageFolder"];
        }

        private string UploadFile(IFormFile? imageFile)
        {
            string uniqueFileName = string.Empty;
            if (imageFile != null)
            {
                uniqueFileName = $"{Guid.NewGuid()}-{imageFile.FileName}";
                using (FileStream fileStream = new FileStream(
                    $"{webHostEnv.WebRootPath}\\{imageFolder}\\{uniqueFileName}", FileMode.CreateNew
                    ))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Dokter API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"DokterModel.GetAll: {e.Message}");
            }
        }

        public async Task<List<VMMLocation>?> GetAllLocation()
        {
            VMResponse<List<VMMLocation>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Doctor/GetAllLocation");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMLocation>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Dokter API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"DokterModel.GetAllLocation: {e.Message}");
            }
        }


        public async Task<VMMLocation?> GetLocationById(string? id)
        {
            VMResponse<VMMLocation>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Doctor/GetLocationById/{id}");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMLocation>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Dokter API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"DokterModel.GetAllLocation: {e.Message}");
            }
        }

        public async Task<VMMSpecialization?> GetSpecializationById(string? id)
        {
            VMResponse<VMMSpecialization>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}Specialization/{id}");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMSpecialization>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Specialization API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"DokterModel.GetSpecializationById: {e.Message}");
            }
        }


        public async Task<List<VMMDoctor>?> GetByFilter(string? location, string? doctorName, string? specialization, string? treatment)
        {
            List<VMMDoctor>? resultData = null;
            VMResponse<List<VMMDoctor>>? apiResponse = new VMResponse<List<VMMDoctor>>(); // Initialize apiResponse

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

            return apiResponse?.Data ?? resultData;
        }

    }
}
