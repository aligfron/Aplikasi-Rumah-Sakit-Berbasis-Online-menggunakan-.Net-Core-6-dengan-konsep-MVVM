using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

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
    }
}
