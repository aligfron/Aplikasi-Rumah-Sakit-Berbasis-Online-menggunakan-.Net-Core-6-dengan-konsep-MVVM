using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace HealthCare340B.Web.Models
{
    public class CustomerMemberModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;

        private HttpContent _content;
        private string _jsonData;

        public CustomerMemberModel(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<List<VMMCustomerMember>> GetAll()
        {
            List<VMMCustomerMember>? data = null;

            try
            {
                VMResponse<List<VMMCustomerMember>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMCustomerMember>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}CustomerMember")
                        .Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Customer Member API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerMemberModel.GetAll: " + e.Message);
                throw new Exception("CustomerMemberModel.GetAll: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMMCustomerMember>> GetByFilter(string filter)
        {
            List<VMMCustomerMember>? data = null;
            try
            {
                VMResponse<List<VMMCustomerMember>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMCustomerMember>>
                >(
                    await _httpClient
                        .GetAsync(
                            (string.IsNullOrEmpty(filter))
                                ? $"{_apiUrl}CustomerMember"
                                : $"{_apiUrl}CustomerMember/GetByFilter/{filter}"
                        )
                        .Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Customer Member API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerMemberModel.GetByFilter: " + e.Message);
                throw new Exception("CustomerMemberModel.GetByFilter: " + e.Message);
            }

            return data;
        }
    }
}
