using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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
        private bool DeleteOldImage(string oldImageFileName)
        {
            try
            {
                oldImageFileName = $"{webHostEnv.WebRootPath}\\{imageFolder}\\{oldImageFileName}";
                if (File.Exists(oldImageFileName))
                {
                    File.Delete(oldImageFileName);
                }
                else
                {
                    throw new ArgumentException("Product Api could");
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        internal async Task<VMResponse<VMMBiodatum>> UpdateAsync(VMMBiodatum data)
        {
            VMResponse<VMMBiodatum>? apiResponse = new VMResponse<VMMBiodatum>();
            try
            {
                if (data.ImageFile != null)
                {
                    if (data.ImagePath != null)
                    {
                        DeleteOldImage(data.ImagePath);
                    }
                    data.ImagePath = UploadFile(data.ImageFile);
                    data.ImageFile = null;

                    //manggil api update proses
                    jsonData = JsonConvert.SerializeObject(data);
                    content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMBiodatum>?>(
                        await httpClient.PutAsync($"{apiUrl}DokterProfil", content).Result.Content.ReadAsStringAsync()
                        );
                }
                else
                {
                    jsonData = JsonConvert.SerializeObject(data);
                    content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMBiodatum>?>(
                        await httpClient.PutAsync($"{apiUrl}DokterProfil", content).Result.Content.ReadAsStringAsync()
                        );
                }
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }

                }
                else
                {
                    throw new Exception("Photo api could not be reached");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Photo.GetbyId: {ex.Message}");
            }
            return apiResponse;
        }

        // ali model spesialisasi doctor
        public async Task<VMTCurrentDoctorSpecialization?> GetByIdSpecializationDoctor(int id)
        {
            VMTCurrentDoctorSpecialization? data = null;
            try
            {
                VMResponse<VMTCurrentDoctorSpecialization>? apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTCurrentDoctorSpecialization>>(

                    await httpClient.GetStringAsync(apiUrl + "SpecializationDoctor/" + id));

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
                Console.WriteLine($"SpecializationDoctorModel.GetAll : {ex.Message}");
            }
            return data;
        }
        public async Task<VMResponse<VMTCurrentDoctorSpecialization>?> CreateSpecializationDoctorAsync(VMTCurrentDoctorSpecialization data)
        {
            VMResponse<VMTCurrentDoctorSpecialization>? apiResponse = new VMResponse<VMTCurrentDoctorSpecialization>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTCurrentDoctorSpecialization>?>(
                    await httpClient.PostAsync($"{apiUrl}SpecializationDoctor", content).Result.Content.ReadAsStringAsync()
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.Created)
                    {
                        throw new Exception(apiResponse.Message);
                    }

                }
                else
                {
                    throw new Exception("Specialization api could not be reached");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SpecializationModel.GetbyId: {ex.Message}");
            }
            return apiResponse;
        }
        public async Task<VMResponse<VMTCurrentDoctorSpecialization>?> EditSpecializationDoctorAsync(VMTCurrentDoctorSpecialization data)
        {
            VMResponse<VMTCurrentDoctorSpecialization>? apiResponse = new VMResponse<VMTCurrentDoctorSpecialization>();
            try
            {
                //manggil api update proses
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTCurrentDoctorSpecialization>?>
                    (await httpClient.PutAsync($"{apiUrl}SpecializationDoctor", content).Result.Content.ReadAsStringAsync());

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {

                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Specialization api could not be reached");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"SpecializationModel.GetbyId: {e.Message}");

            }
            return apiResponse;
        }
    }
}
