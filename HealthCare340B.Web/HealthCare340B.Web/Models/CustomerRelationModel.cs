using System.Net;
using System.Text;
using HealthCare340B.ViewModel;
using Newtonsoft.Json;

namespace HealthCare340B.Web.Models
{
    public class CustomerRelationModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;

        private HttpContent _content;
        private string _jsonData;

        public CustomerRelationModel(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"];
        }

        public async Task<List<VMMCustomerRelation>> GetAll()
        {
            List<VMMCustomerRelation>? data = null;

            try
            {
                VMResponse<List<VMMCustomerRelation>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMCustomerRelation>>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}CustomerRelation")
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK || apiResponse.StatusCode == HttpStatusCode.NoContent)
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
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.GetAll: " + e.Message);
                throw new Exception("CustomerRelationModel.GetAll: " + e.Message);
            }

            return data;
        }

        public async Task<List<VMMCustomerRelation>> GetByFilter(string filter)
        {
            List<VMMCustomerRelation>? data = null;

            try
            {
                VMResponse<List<VMMCustomerRelation>>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<List<VMMCustomerRelation>>
                >(
                    await _httpClient
                        .GetAsync(
                            (string.IsNullOrEmpty(filter))
                                ? $"{_apiUrl}CustomerRelation"
                                : $"{_apiUrl}CustomerRelation/GetByFilter/{filter}"
                        )
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode == HttpStatusCode.OK || apiResponse.StatusCode == HttpStatusCode.NoContent)
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
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.GetByFilter: " + e.Message);
                throw new Exception("CustomerRelationModel.GetByFilter: " + e.Message);
            }

            return data;
        }

        public async Task<VMMCustomerRelation> GetById(long id)
        {
            VMMCustomerRelation? data = null;

            try
            {
                VMResponse<VMMCustomerRelation>? apiResponse = JsonConvert.DeserializeObject<
                    VMResponse<VMMCustomerRelation>
                >(
                    await _httpClient
                        .GetAsync($"{_apiUrl}CustomerRelation/{id}")
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
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.GetById: " + e.Message);
                throw new Exception("CustomerRelationModel.GetById: " + e.Message);
            }

            return data;
        }

        public async Task<bool> CheckNameExistsAsync(string name)
        {
            bool isExists = false;

            try
            {
                List<VMMCustomerRelation>? apiResponse = GetAll().Result;

                if (apiResponse != null)
                {
                    isExists = apiResponse.Any(x => x.Name.ToLower() == name.ToLower());
                }
                else
                {
                    throw new Exception("Customer Relation data could not be retrieved");
                }

            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.CheckNameExistsAsync: " + e.Message);
                throw new Exception("CustomerRelationModel.CheckNameExistsAsync: " + e.Message);
            }

            return isExists;
        }

        public async Task<VMResponse<VMMCustomerRelation>> CreateAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? apiResponse = new VMResponse<VMMCustomerRelation>();

            try
            {
                _jsonData = JsonConvert.SerializeObject(data);
                _content = new StringContent(_jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerRelation>>(
                    await _httpClient
                        .PostAsync($"{_apiUrl}CustomerRelation", _content)
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
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.CreateAsync: " + e.Message);
                throw new Exception("CustomerRelationModel.CreateAsync: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMCustomerRelation>> UpdateAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? apiResponse = new VMResponse<VMMCustomerRelation>();

            try
            {
                _jsonData = JsonConvert.SerializeObject(data);
                _content = new StringContent(_jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerRelation>>(
                    await _httpClient
                        .PutAsync($"{_apiUrl}CustomerRelation", _content)
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
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.UpdateAsync: " + e.Message);
                throw new Exception("CustomerRelationModel.UpdateAsync: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMCustomerRelation>> DeleteAsync(long id, long userId)
        {
            VMResponse<VMMCustomerRelation>? apiResponse = new VMResponse<VMMCustomerRelation>();

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMCustomerRelation>>(
                    await _httpClient
                        .DeleteAsync($"{_apiUrl}CustomerRelation/{id}/{userId}")
                        .Result.Content.ReadAsStringAsync()
                );

                if (apiResponse != null)
                {
                    if (apiResponse.StatusCode != HttpStatusCode.OK && apiResponse.StatusCode != HttpStatusCode.Conflict)
                    {
                        throw new Exception(apiResponse.Message);
                    }
                }
                else
                {
                    throw new Exception("Customer Relation API could not be reached");
                }
            }
            catch (Exception e)
            {
                // Logging
                Console.WriteLine("CustomerRelationModel.DeleteAsync: " + e.Message);
                throw new Exception("CustomerRelationModel.DeleteAsync: " + e.Message);
            }

            return apiResponse;
        }
    }
}
