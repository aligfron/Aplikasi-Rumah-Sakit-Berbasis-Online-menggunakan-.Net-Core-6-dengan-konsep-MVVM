using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class ForgotPasswordModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        HttpContent content;
        private string jsonData;
        public ForgotPasswordModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<VMResponse<VMTToken>> SendEmailAsync(string email)
        {
            VMResponse<VMTToken> apiResponse = new VMResponse<VMTToken>();
            try
            {
                jsonData = JsonConvert.SerializeObject(email);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTToken>?>(
                              await httpClient
                              .PostAsync($"{apiUrl}ForgotPassword/GenerateOTP/{email}", content)
                              .Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.Created)
                    {
                        return apiResponse;
                    }

                }
                else
                {
                    throw new Exception("API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User API cannot be reached! {ex.Message}");
                throw new Exception($"User API cannot be reached! {ex.Message}");
            }
            return apiResponse;
        }

        public async Task<VMResponse<VMTToken>> VerifyOtpAsync(string OTP)
        {
            VMResponse<VMTToken> apiResponse = new VMResponse<VMTToken>();
            try
            {
                jsonData = JsonConvert.SerializeObject(OTP);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{apiUrl}ForgotPassword/VerifyOTP/{OTP}", content);
                var responseData = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTToken>?>(responseData);
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return apiResponse;
                    }
                }
                else
                {
                    throw new Exception("API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User API cannot be reached! {ex.Message}");
                throw new Exception($"User API cannot be reached! {ex.Message}");
            }
            return apiResponse;
        }

        public async Task<VMResponse<VMMUser>> ConfirmPasswordAsync(string password, string confirmPassword)
        {
            VMResponse<VMMUser> apiResponse = new VMResponse<VMMUser>();
            try
            {
                var jsonData = JsonConvert.SerializeObject(new { Password = password, ConfirmPassword = confirmPassword });
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMUser>?>(
                              await httpClient
                              .PostAsync($"{apiUrl}ForgotPassword/ConfirmPassword/{password}/{confirmPassword}", content)
                              .Result.Content.ReadAsStringAsync()
                );
                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return apiResponse;
                    }
                }
                else
                {
                    throw new Exception("API cannot be reached!");
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
