using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class PaymentMethodModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        HttpContent content;
        private string jsonData;

        public PaymentMethodModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<List<VMMPaymentMethod>?> GetByFilter(string? filter)
        {
            List<VMMPaymentMethod>? data = null;
            VMResponse<List<VMMPaymentMethod>>? apiResponse = new VMResponse<List<VMMPaymentMethod>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync((string.IsNullOrEmpty(filter))
                        ? $"{apiUrl}PaymentMethod"
                        : $"{apiUrl}PaymentMethod/GetByFilter/{filter}"
                    );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMPaymentMethod>>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No data is found within payment method");
                    }
                }
                else
                {
                    throw new Exception("Payment Method API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("PaymentMethodModel.GetByFilter: " + e.Message);
                //throw new Exception("CategoryModel.GetByFilter: " + e.Message);
            }

            return data;
        }

        public async Task<VMMPaymentMethod?> GetById(long id)
        {
            VMMPaymentMethod? data = null;
            VMResponse<VMMPaymentMethod>? apiResponse = new VMResponse<VMMPaymentMethod>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                    $"{apiUrl}PaymentMethod/GetById/{id}"
                    );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMPaymentMethod>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No data is found within payment method");
                    }
                }
                else
                {
                    throw new Exception("Payment Method API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("PaymentMethodModel.GetById: " + e.Message);
                //throw new Exception("CategoryModel.GetByFilter: " + e.Message);
            }

            return data;
        }

        public async Task<VMResponse<VMMPaymentMethod>?> Create(VMMPaymentMethod data)
        {
            VMResponse<VMMPaymentMethod>? apiResponse = new VMResponse<VMMPaymentMethod>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMPaymentMethod>?>(     // Convert the Json string to a class
                        await httpClient.PostAsync($"{apiUrl}PaymentMethod", content)    // Call the API
                        .Result                                                     // Read the Result
                        .Content                                                    // Get the Content Result
                        .ReadAsStringAsync()                                        // Convert the content as string
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
                    throw new Exception("Payment Method API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("PaymentMethodModel.Create: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMPaymentMethod>?> Update(VMMPaymentMethod data)
        {
            VMResponse<VMMPaymentMethod>? apiResponse = new VMResponse<VMMPaymentMethod>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                    JsonConvert.DeserializeObject<VMResponse<VMMPaymentMethod>?>(     // Convert the Json string to a class
                        await httpClient.PutAsync($"{apiUrl}PaymentMethod", content)    // Call the API
                        .Result                                                     // Read the Result
                        .Content                                                    // Get the Content Result
                        .ReadAsStringAsync()                                        // Convert the content as string
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
                    throw new Exception("Payment Method API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("PaymentMethodModel.Update: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMPaymentMethod>?> Delete(long id, long userId)
        {
            VMResponse<VMMPaymentMethod>? apiResponse = new VMResponse<VMMPaymentMethod>();

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMPaymentMethod>?>(     // Convert the Json string to a class
                        await httpClient.DeleteAsync($"{apiUrl}PaymentMethod?id={id}&deletedBy={userId}")    // Call the API
                        .Result                                                     // Read the Result
                        .Content                                                    // Get the Content Result
                        .ReadAsStringAsync()                                        // Convert the content as string
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
                    throw new Exception("Category API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("PaymentMethodModel.Delete: " + e.Message);
            }

            return apiResponse;
        }
    }
}
