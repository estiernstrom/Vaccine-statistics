using DSU24_Grupp5.Models;
using DSU24_Grupp5.Models.Dto;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace DSU24_Grupp5.Infrastructure
{
    public static class ApiEngine
    {
        public async static Task<ApiResponse<T>> Post<T>(string apiUrl, string body)
        {
            using HttpClient client = new HttpClient();
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out _))
                {
                    apiResponse.ErrorMessage = "Invalid URL";
                    HttpResponseMessage invalidUrlResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "Invalid URL"
                    };

                    apiResponse.Response = invalidUrlResponse;
                    return apiResponse;
                }

                HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    apiResponse.Data = JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            catch (Exception)
            {
                apiResponse.ErrorMessage = "Något gick fel, vänligen försök igen";

            }

            return apiResponse;
        }
        public async static Task<ApiResponse<T>> Fetch<T>(string apiUrl)
        {
            using HttpClient client = new HttpClient();
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out _))
                {
                    apiResponse.ErrorMessage = "Invalid URL";
                    HttpResponseMessage invalidUrlResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "Invalid URL"
                    };

                    apiResponse.Response = invalidUrlResponse;
                    return apiResponse;
                }

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    apiResponse.Data = JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            catch (Exception)
            {
                apiResponse.ErrorMessage = "Något gick fel, vänligen försök igen";

            }
            return apiResponse;
        }
    }
}
