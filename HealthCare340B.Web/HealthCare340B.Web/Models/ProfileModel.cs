using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{
    public class ProfileModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        HttpContent content;
        private string jsonData;
        private VMResponse<List<VMMSpecialization>>? apiResponse;
        private readonly IWebHostEnvironment webHostEnv;
        private readonly string imageFolder;

        public ProfileModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
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
        //aaa
        public async Task<VMMDoctor?> GetByIdProfilDokter(int id)
        {
            VMMDoctor? data = null;
            try
            {
                VMResponse<VMMDoctor>? apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMDoctor>>(

                    await httpClient.GetStringAsync(apiUrl + "DokterProfil/" + id));

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
            }
            catch
                (Exception ex)
            {
                Console.WriteLine($"DokterProfilModel.GetAll : {ex.Message}");
            }
            return data;
        }
    }
}
