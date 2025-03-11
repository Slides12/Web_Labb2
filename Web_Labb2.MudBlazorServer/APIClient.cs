using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.BlazorServer
{
    public class APIClient(HttpClient httpClient, ProtectedLocalStorage localStorage)
    {

        public async Task SetAuthorizedHeader()
        {
            var token = (await localStorage.GetAsync<string>("authToken")).Value;
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        public async Task<T> GetFromJsonAsync<T>(string path)
        {
            await SetAuthorizedHeader();
            return await httpClient.GetFromJsonAsync<T>(path);
        }

        public async Task<T1> PostAsync<T1, T2>(string path, T2 postModel)
        {
            await SetAuthorizedHeader();

            try
            {
                // Serialize request model to JSON
                var json = JsonConvert.SerializeObject(postModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send HTTP POST request
                var response = await httpClient.PostAsync(path, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                // Log response details (for debugging)
                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Response Content-Type: {response.Content.Headers.ContentType}");
                Console.WriteLine($"Response JSON: {responseJson}");

                // Throw exception if the response is not successful
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.StatusCode}, Response: {responseJson}");
                }

                // Handle empty response
                if (string.IsNullOrWhiteSpace(responseJson))
                {
                    return default(T1);
                }

                // Deserialize JSON response
                return JsonConvert.DeserializeObject<T1>(responseJson);
            }
            catch (JsonException ex)
            {
                throw new Exception($"JSON Parsing Error: {ex.Message}. Response: {path}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP Request Error: {ex.Message}");
            }
        }


        public async Task<string> PostFileAsync(string path, IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);
            using var streamContent = new StreamContent(fileStream);

            content.Add(streamContent, "file", file.Name);

            var response = await httpClient.PostAsync(path, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error uploading file: {response.StatusCode}");
                return string.Empty;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"PostFileAsync Response: {responseJson}");

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseJson);

            if (result != null && result.TryGetValue("imagePath", out string? imagePath))
            {
                Console.WriteLine($"Parsed Image Path: {imagePath}");
                return imagePath;
            }

            Console.WriteLine("Failed to parse image path from response.");
            return string.Empty;
        }




        public async Task<T1> PutAsync<T1, T2>(string path, T2 postModel)
        {
            await SetAuthorizedHeader();

            var response = await httpClient.PutAsJsonAsync(path, postModel);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {response.StatusCode} - {errorContent}");
                throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from API: {responseJson}");

            if (!responseJson.StartsWith("{") && !responseJson.StartsWith("["))
            {
                Console.WriteLine("Received plain text response: " + responseJson);
                return default(T1);
            }

            try
            {
                return JsonConvert.DeserializeObject<T1>(responseJson);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Failed to deserialize response. Raw response: {responseJson}");
                throw new Exception("JSON deserialization error", ex);
            }
        }



        public async Task<bool> DeleteAsync(string path)
        {
            await SetAuthorizedHeader();
            var response = await httpClient.DeleteAsync(path);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
