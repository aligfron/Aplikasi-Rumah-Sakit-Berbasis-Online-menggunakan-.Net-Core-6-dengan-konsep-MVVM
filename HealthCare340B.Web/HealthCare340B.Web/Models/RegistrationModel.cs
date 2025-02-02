﻿using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace HealthCare340B.Web.Models
{
    public class RegistrationModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;

        HttpContent content;
        private string jsonData;

        public RegistrationModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse<VMTToken>> EmailConfirmAsync(string email) 
        {
            VMResponse<VMTToken> apiResponse = new VMResponse<VMTToken>();
            try 
            {
                jsonData = JsonConvert.SerializeObject(email);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMTToken>?>(
                              await httpClient
                              .PostAsync($"{apiUrl}Register/GenerateOTP/{email}", content)
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
            catch(Exception ex) 
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
                var response = await httpClient.PostAsync($"{apiUrl}Register/VerifyOTP/{OTP}", content);
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
            catch(Exception ex)
            {
                Console.WriteLine($"User API cannot be reached! {ex.Message}");
                throw new Exception($"User API cannot be reached! {ex.Message}");
            }
            return apiResponse;
        }
        //List<string> confirm = new List<string> {string password };
        
        public async Task<VMResponse<VMMUser>> ConfirmPasswordAsync(string password, string confirmPassword) 
        {
            VMResponse<VMMUser> apiResponse = new VMResponse<VMMUser>();
            try 
            {
                var jsonData = JsonConvert.SerializeObject(new { Password = password, ConfirmPassword = confirmPassword });
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMUser>?>(
                              await httpClient
                              .PostAsync($"{apiUrl}Register/ConfirmPassword/{password}/{confirmPassword}", content)
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
        public async Task<VMResponse<VMMUser>> SignUpAsync(VMMUser data) 
        {
            VMResponse<VMMUser> apiResponse = new VMResponse<VMMUser>();
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse<VMMUser>?>(
                              await httpClient
                              .PostAsync($"{apiUrl}Register/Register", content)
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
