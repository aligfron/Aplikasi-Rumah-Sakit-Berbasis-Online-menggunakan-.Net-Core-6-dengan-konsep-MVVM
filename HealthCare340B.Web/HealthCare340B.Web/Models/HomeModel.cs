using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{

    public class HomeModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        public HomeModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public List<VMMMenuRole>? getAll()
        {
            List<VMMMenuRole>? data = null;
            try
            {
                data = JsonConvert.DeserializeObject<List<VMMMenuRole>>(httpClient.GetStringAsync(apiUrl + "Home").Result);

            }
            catch (Exception e)
            {
                throw new Exception($"HomeModelGetAll:{e.Message}");
            }
            return data;
        }
        public async Task<List<VMMMenuRole>?> GetByFilter(string? filter)
        {
            List<VMMMenuRole>? data = null;

            try
            {
                VMResponse<List<VMMMenuRole>>? apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<List<VMMMenuRole>>?>(
                        await httpClient.GetStringAsync(
                            (string.IsNullOrEmpty(filter))
                            ? $"{apiUrl}Menu"
                            : $"{apiUrl}Menu/GetByFilter/{filter}"
                        )
                    );

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
                else
                {
                    throw new Exception("Menu API could not be reached!");
                }
            }
            catch (Exception e)
            {

                //Logging
                Console.WriteLine($"Menu:{e.Message}");
                throw new Exception($"MenuModel.GetByFilter: {e.Message}");
            }

            return data;
        }
        public async Task<VMMMenuRole?> GetById(int id)
        {
            VMMMenuRole? data = null;

            try
            {
                VMResponse<VMMMenuRole>? apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMMenuRole>?>(
                        await httpClient.GetStringAsync($"{apiUrl}Menu/{id}")
                    );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        data = apiResponse.Data;
                    }
                    else
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Menu API could not be reached!");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine($"HomeModel.GetById: {e.Message}");
                throw new Exception($"HomeModel.GetByFilter: {e.Message}");
            }

            return data;
        }

    }
}
