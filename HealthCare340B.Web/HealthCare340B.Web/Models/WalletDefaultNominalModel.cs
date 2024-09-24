using HealthCare340B.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HealthCare340B.Web.Models
{
    public class WalletDefaultNominalModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        HttpContent content;
        private string jsonData;

        public WalletDefaultNominalModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }

        public async Task<List<VMMWalletDefaultNominal>?> GetByFilter(int? nominal)
        {
            List<VMMWalletDefaultNominal>? data = null;
            VMResponse<List<VMMWalletDefaultNominal>>? apiResponse = new VMResponse<List<VMMWalletDefaultNominal>>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync((nominal == null)
                      ? $"{apiUrl}WalletDefaultNominal"
                      : $"{apiUrl}WalletDefaultNominal/GetByFilter/{nominal}"
                  );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<List<VMMWalletDefaultNominal>>?>
                            (apiResponseMsg.Content.ReadAsStringAsync().Result);

                        data = apiResponse!.Data;
                    }
                    else
                    {
                        throw new Exception($"{HttpStatusCode.NoContent} - No data is found within Wallet Default Nominal");
                    }
                }
                else
                {
                    throw new Exception("Wallet Default Nominal API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("Wallet Default NominalModel.GetByFilter: " + e.Message);
                //throw new Exception("Wallet Default Nominal.GetByFilter: " + e.Message);
            }
            return data;
        }

        public async Task<VMMWalletDefaultNominal?> GetById(long id)
        {
            VMMWalletDefaultNominal? data = null;
            VMResponse<VMMWalletDefaultNominal>? apiResponse = new VMResponse<VMMWalletDefaultNominal>();
            try
            {
                HttpResponseMessage apiResponseMsg = await httpClient.GetAsync(
                   $"{apiUrl}WalletDefaultNominal/GetById/{id}"
                   );

                if (apiResponseMsg != null)
                {
                    if (apiResponseMsg.StatusCode == HttpStatusCode.OK)
                    {
                        apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMWalletDefaultNominal>?>
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
                Console.WriteLine("WalletDefaultNominalModel.GetById: " + e.Message);
                //throw new Exception("CategoryModel.GetByFilter: " + e.Message);
            }
            return data;
        }

        public async Task<VMResponse<VMMWalletDefaultNominal>?> Create(VMMWalletDefaultNominal data)
        {
            VMResponse<VMMWalletDefaultNominal>? apiResponse = new VMResponse<VMMWalletDefaultNominal>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                   JsonConvert.DeserializeObject<VMResponse<VMMWalletDefaultNominal>?>(     // Convert the Json string to a class
                       await httpClient.PostAsync($"{apiUrl}WalletDefaultNominal", content)    // Call the API
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
                    throw new Exception("Wallet Default Nominal API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("WalletDefaultNominal.Create: " + e.Message);
            }
            return apiResponse;
        }

        public async Task<VMResponse<VMMWalletDefaultNominal>?> Update(VMMWalletDefaultNominal data)
        {
            VMResponse<VMMWalletDefaultNominal>? apiResponse = new VMResponse<VMMWalletDefaultNominal>();

            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(    // Create a blank HttpRequest Document
                    jsonData,               // Put the Request Body (data)
                    Encoding.UTF8,          // Assign the Request body's character set
                    "application/json"      // Assain the Request body's format / Content-Type
                    );

                apiResponse =
                  JsonConvert.DeserializeObject<VMResponse<VMMWalletDefaultNominal>?>(     // Convert the Json string to a class
                      await httpClient.PutAsync($"{apiUrl}WalletDefaultNominal", content)    // Call the API
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
                    throw new Exception("Wallet Default Nominal API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("WalletDefaultNominal.Update: " + e.Message);
            }

            return apiResponse;
        }

        public async Task<VMResponse<VMMWalletDefaultNominal>?> Delete(long id, long userId)
        {
            VMResponse<VMMWalletDefaultNominal>? apiResponse = new VMResponse<VMMWalletDefaultNominal>();

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMWalletDefaultNominal>?>(     // Convert the Json string to a class
                       await httpClient.DeleteAsync($"{apiUrl}WalletDefaultNominal?id={id}&deletedBy={userId}")    // Call the API
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
                    throw new Exception("Wallet Default Nominal API could not be reached");
                }
            }
            catch (Exception e)
            {
                //Logging
                Console.WriteLine("WalletDefaultNominal.Delete: " + e.Message);
            }

            return apiResponse;
        }
    }
}
