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

                var json = JsonConvert.SerializeObject(postModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(path, content);
                var responseJson = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.StatusCode}, Response: {responseJson}");
                }

                if (string.IsNullOrWhiteSpace(responseJson))
                {
                    return default(T1);
                }

                return JsonConvert.DeserializeObject<T1>(responseJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}. Response: {path}");
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
                Console.WriteLine($"Error saving file: {response.StatusCode}");
                return string.Empty;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseJson}");

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseJson);

            if (result != null && result.TryGetValue("imagePath", out string? imagePath))
            {
                Console.WriteLine($"Parsed Image Path: {imagePath}");
                return imagePath;
            }

            Console.WriteLine("Couldnt parse imagepath.");
            return string.Empty;
        }




        public async Task<T1> PutAsync<T1, T2>(string path, T2 postModel)
        {
            await SetAuthorizedHeader();

            var response = await httpClient.PutAsJsonAsync(path, postModel);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error from API: {response.StatusCode} - {errorContent}");
                throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from API: {responseJson}");

            try
            {
                if (typeof(T1) == typeof(string))
                {
                    return default(T1);
                }

                return JsonConvert.DeserializeObject<T1>(responseJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to deserialize response. Response: {responseJson}");
                throw new Exception("Error", ex);
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
