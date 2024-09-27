using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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

        public async Task<List<VMMCustomerMember>> GetAll(long parentId)
        {
            List<VMMCustomerMember>? data = null;

            try
            {
                VMResponse<List<VMMCustomerMember>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMCustomerMember>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}CustomerMember/{parentId}")
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

        public async Task<List<VMMCustomerMember>> GetByFilter(string filter, long parentId)
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
                                ? $"{_apiUrl}CustomerMember/{parentId}"
                                : $"{_apiUrl}CustomerMember/GetByFilter/{parentId}/{filter}"
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

        public async Task<VMMCustomerMember> GetById(long id, long parentId)
        {
            VMMCustomerMember? data = null;

            try
            {
                VMResponse<VMMCustomerMember>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<VMMCustomerMember>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}CustomerMember/{id}/{parentId}")
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
                Console.WriteLine("CustomerMemberModel.GetById: " + e.Message);
                throw new Exception("CustomerMemberModel.GetById: " + e.Message);
            }

            return data;
        }

        public async Task<VMResponse<VMMCustomerMember>> CreateAsync(VMMCustomerMember data)
        {
            VMResponse<VMMCustomerMember>? apiResponse = new VMResponse<VMMCustomerMember>();

            try
            {
                _jsonData = JsonConvert.SerializeObject(data);
                _content = new StringContent(_jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerMember>>(
                    await _httpClient
                        .PostAsync($"{_apiUrl}CustomerMember", _content)
                        .Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Customer Member API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerMemberModel.CreateAsync: " + e.Message);
                throw new Exception("CustomerMemberModel.CreateAsync: " + e.Message);
            }

            return apiResponse;

        }

        public async Task<VMResponse<VMMCustomerMember>> UpdateAsync(VMMCustomerMember data)
        {
            VMResponse<VMMCustomerMember>? apiResponse = new VMResponse<VMMCustomerMember>();

            try
            {
                _jsonData = JsonConvert.SerializeObject(data);
                _content = new StringContent(_jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerMember>>(
                    await _httpClient
                        .PutAsync($"{_apiUrl}CustomerMember", _content)
                        .Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Customer Member API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerMemberModel.UpdateAsync: " + e.Message);
                throw new Exception("CustomerMemberModel.UpdateAsync: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMCustomerMember>> DeleteAsync(long id, long userId)
        {
            VMResponse<VMMCustomerMember>? apiResponse = new VMResponse<VMMCustomerMember>();

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerMember>>(
                    await _httpClient
                        .DeleteAsync($"{_apiUrl}CustomerMember/{id}/{userId}")
                        .Result.Content.ReadAsStringAsync()
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
                    throw new Exception("Customer Member API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerMemberModel.DeleteAsync: " + e.Message);
                throw new Exception("CustomerMemberModel.DeleteAsync: " + e.Message);
            }

            return apiResponse;
        }
    }
}
