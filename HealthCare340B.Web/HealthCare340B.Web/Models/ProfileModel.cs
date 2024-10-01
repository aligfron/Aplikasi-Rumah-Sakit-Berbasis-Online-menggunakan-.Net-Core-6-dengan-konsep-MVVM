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
        public async Task<VMMDoctor?> GetByIdProfilDokter(long? id)
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
        public async Task<VMMDoctor?> GetDoctorByBiodataId(int idBiodata)
        {
            VMMDoctor? data = null;
            try
            {
                VMResponse<VMMDoctor>? apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMDoctor>>(

                    await httpClient.GetStringAsync(apiUrl + "DokterProfil/GetDoctorByBiodataId/" + idBiodata));

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




        // punya faraday

        public async Task<List<VMMBiodataAddress>?> GetAllBioAddress()
        {
            VMResponse<List<VMMBiodataAddress>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}TabAlamat/GetAll");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMBiodataAddress>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                        return apiResponse?.Data;
                    }
                    else
                    {
                        throw new Exception($"Error: {apiResponseMsg.StatusCode}, {await apiResponseMsg.Content.ReadAsStringAsync()}");
                    }
                }
                else
                {
                    throw new Exception("Tab Alamat API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ProfilModel.GetAllBioAddress: {e.Message}");
            }
        }

        public async Task<List<VMMLocation>?> GetAllLocation()
        {
            VMResponse<List<VMMLocation>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync($"{apiUrl}TabAlamat/GetAllLocation");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMLocation>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        apiResponse!.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    throw new Exception("Tab Alamat (GetAllLocation) API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Profile.GetAllLocation: {e.Message}");
            }

            return apiResponse!.Data;

        }

        public async Task<List<VMMBiodataAddress>?> GetByFilter(string filter)
        {
            VMResponse<List<VMMBiodataAddress>>? apiResponse = null;
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                            (string.IsNullOrEmpty(filter))
                            ? $"{apiUrl}TabAlamat/GetAll"
                            : $"{apiUrl}TabAlamat/GetByFilter/{filter}");

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMBiodataAddress>>?>(await apiResponseMsg.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        apiResponse!.StatusCode = apiResponseMsg.StatusCode;
                        apiResponse.Message = await apiResponseMsg.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    throw new Exception("Tab Alamat  API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Profile.Get: {e.Message}");
            }

            return apiResponse!.Data;

        }

        public async Task<VMResponse<VMMBiodataAddress>?> CreateAsync(VMMBiodataAddress data)
        {
            VMResponse<VMMBiodataAddress>? apiResponse = new VMResponse<VMMBiodataAddress>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                );

                apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMBiodataAddress>?>(
                        await httpClient.PostAsync($"{apiUrl}TabAlamat", content)
                            .Result
                            .Content
                            .ReadAsStringAsync()
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
                    throw new Exception("TabAlamat API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ProfileModel.CreateAsync: {e.Message}");
            }

            return apiResponse;
        }

        public async Task<VMMBiodataAddress?> GetByIdAlamat(int id)
        {
            VMMBiodataAddress? data = null;

            try
            {
                VMResponse<VMMBiodataAddress>? apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMBiodataAddress>?>(
                        await httpClient.GetStringAsync($"{apiUrl}TabAlamat/GetById/{id}")
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                        data = apiResponse.Data;
                    else
                        throw new Exception(apiResponse.Message);
                }
                else
                {
                    throw new Exception("TabAlamat API cannot be reached!");
                }

            }
            catch (Exception e)
            {
                //Logging
                throw new Exception($"ProfileModel.GetByIdAlamat: {e.Message}");
            }

            return data;
        }

        public async Task<VMMCustomer?> GetByIdCustomerProfile(int id)
        {
            VMMCustomer? data = null;

            try
            {
                VMResponse<VMMCustomer>? apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMCustomer>?>(
                        await httpClient.GetStringAsync($"{apiUrl}TabProfile/GetById/{id}")
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                        data = apiResponse.Data;
                    else
                        throw new Exception(apiResponse.Message);
                }
                else
                {
                    throw new Exception("TabProfile API cannot be reached!");
                }

            }
            catch (Exception e)
            {
                //Logging
                throw new Exception($"ProfileModel.GetByIdAlamat: {e.Message}");
            }

            return data;
        }


        public async Task<VMMCustomer?> GetCustomerByBioId(int bioId)
        {
            VMMCustomer? data = null;

            try
            {
                VMResponse<VMMCustomer>? apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMCustomer>?>(
                        await httpClient.GetStringAsync($"{apiUrl}TabProfile/GetCustomerByBioId/{bioId}")
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                        data = apiResponse.Data;
                    else
                        throw new Exception(apiResponse.Message);
                }
                else
                {
                    throw new Exception("TabProfile API cannot be reached!");
                }

            }
            catch (Exception e)
            {
                //Logging
                throw new Exception($"ProfileModel.GetByIdAlamat: {e.Message}");
            }

            return data;
        }

        public async Task<VMResponse<VMMCustomer>?> UpdateProfileAsync(VMMCustomer data)
        {
            VMResponse<VMMCustomer>? apiResponse = new VMResponse<VMMCustomer>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomer>?>(
                    await httpClient.PutAsync($"{apiUrl}TabProfile/Update", content)
                    .Result
                        .Content
                        .ReadAsStringAsync()

                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("TabProfile API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ProfileModel.UpdateProfileAsync: {e.Message}");

            }
            return apiResponse;
        }



        public async Task<VMResponse<VMMBiodataAddress>?> UpdateAsync(VMMBiodataAddress data)
        {
            VMResponse<VMMBiodataAddress>? apiResponse = new VMResponse<VMMBiodataAddress>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMBiodataAddress>?>(
                    await httpClient.PutAsync($"{apiUrl}TabAlamat/Update", content)
                    .Result
                        .Content
                        .ReadAsStringAsync()

                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("TabAlamat API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ProfileModel.UpdateAsync: {e.Message}");

            }
            return apiResponse;
        }

        public async Task<VMResponse<VMMBiodataAddress>?> DeleteAsync(int id, int userId)
        {
            VMResponse<VMMBiodataAddress>? apiResponse = new VMResponse<VMMBiodataAddress>();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMBiodataAddress>?>(
                    await httpClient.DeleteAsync($"{apiUrl}TabAlamat/{id}/{userId}")
                    .Result
                    .Content
                    .ReadAsStringAsync()
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Variant API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"VariantModel.DeleteAsync: {e.Message}");
            }

            return apiResponse;
        }

        public class MultipleDeleteRequest
        {
            public List<long> Ids { get; set; }
            public long UserId { get; set; }
        }

        public async Task<VMResponse<List<VMMBiodataAddress>>?> MultipleDeleteAsync(VMMBiodataAddress.MultipleDeleteRequest requestData)
        {
            VMResponse<List<VMMBiodataAddress>>? apiResponse = new VMResponse<List<VMMBiodataAddress>>();
            try
            {
                jsonData = JsonConvert.SerializeObject(requestData);
                content = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMBiodataAddress>>?>(
                    await httpClient.PutAsync($"{apiUrl}TabAlamat/MultipleDelete", content)
                    .Result
                        .Content
                        .ReadAsStringAsync()

                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("TabAlamat API could not be reached!");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ProfileModel.MultipleDeleteAsync: {e.Message}");

            }

            return apiResponse;
        }
    }
}
