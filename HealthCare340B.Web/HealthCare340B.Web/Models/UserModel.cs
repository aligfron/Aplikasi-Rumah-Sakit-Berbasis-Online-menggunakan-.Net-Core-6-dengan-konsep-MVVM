using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HealthCare340B.Web.Models
{

    public class UserModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        HttpContent content;
        private string jsonData;

        public UserModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMMUser> GetByEmail(string email)
        {
            VMMUser? data = null;

            try
            {

                HttpResponseMessage apiResponseMsg =
                    await httpClient.GetAsync($"{apiUrl}User/GetByEmail/{email}");
                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        VMResponse<VMMUser>? apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMUser>>
                             (apiResponseMsg.Content.ReadAsStringAsync().Result);
                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{apiResponseMsg.StatusCode} - {apiResponseMsg.Content.ReadAsStringAsync().Result}");
                    }
                }
                else
                {
                    throw new Exception($"{apiResponseMsg.StatusCode} - {apiResponseMsg.RequestMessage}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return data;
        }
        public async Task<VMResponse<VMMUser>> LoginAsync(VMMUser data) 
        {
            VMResponse<VMMUser> apiResponse = new VMResponse<VMMUser>();
            try 
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMUser>?>(
                              await httpClient
                              .PutAsync($"{apiUrl}User/Login", content)
                              .Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK)
                    {
                        
                        throw new Exception(apiResponse.Message);
                    }
                    if (apiResponse.Data.IsLocked == true)
                    {
                        throw new Exception("Your Account is Locked");
                    }
                }
                else
                {
                    throw new Exception("User API cannot be reached!");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"User API cannot be reached! {ex.Message}");
                throw new Exception($"User API cannot be reached! {ex.Message}");
            }
            return apiResponse;
        } 
    }
}
