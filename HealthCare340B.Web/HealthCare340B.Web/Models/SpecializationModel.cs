using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class SpecializationModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        HttpContent content;
        private string jsonData;
        private VMResponse<List<VMMSpecialization>>? apiResponse;

        public SpecializationModel(IConfiguration _config)
        {
            apiUrl = _config["apiUrl"];
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

        //tambahan Ali
        public async Task<List<VMMSpecialization>>? getByFilter(string? filter)
        {
            List<VMMSpecialization>? dataCoba = null;
            try
            {

                apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMSpecialization>>?>(
                    (string.IsNullOrEmpty(filter))
                    ? await httpClient.GetStringAsync(apiUrl + "Specialization")
                    : await httpClient.GetStringAsync(apiUrl + "Specialization/GetBy/" + filter));

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCoba = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataCoba;
        }
        public async Task<VMMSpecialization?> getById(int id)
        {
            VMMSpecialization? dataCoba = null;
            try
            {

                VMResponse<VMMSpecialization>? apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMSpecialization>>
                    (await httpClient.GetStringAsync(apiUrl + "Specialization/" + id));



                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        dataCoba = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SpecializationModel.GetById : {ex.Message}");
            }
            return dataCoba;
        }
        public async Task<VMResponse<VMMSpecialization>?> CreateAsync(VMMSpecialization data)
        {
            VMResponse<VMMSpecialization>? apiResponse = new VMResponse<VMMSpecialization>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMSpecialization>?>(
                    await httpClient.PostAsync($"{apiUrl}Specialization", content).Result.Content.ReadAsStringAsync()
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
        public async Task<VMResponse<VMMSpecialization>?> UpdateAsync(VMMSpecialization data)
        {
            VMResponse<VMMSpecialization>? apiResponse = new VMResponse<VMMSpecialization>();
            try
            {
                //manggil api update proses
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMSpecialization>?>
                    (await httpClient.PutAsync($"{apiUrl}Specialization", content).Result.Content.ReadAsStringAsync());

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
        public async Task<VMResponse<VMMSpecialization>?> DeleteAsync(int id, int userId)
        {
            VMResponse<VMMSpecialization>? apiResponse = new VMResponse<VMMSpecialization>();
            try
            {

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMSpecialization>?>(
                    await httpClient.DeleteAsync($"{apiUrl}Specialization/{id}/{userId}").Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Specialization api could not be reached");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SpecializationModel.Delete: {ex.Message}");
            }
            return apiResponse;
        }
    }
}
