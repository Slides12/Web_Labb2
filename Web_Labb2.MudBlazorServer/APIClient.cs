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
            var json = JsonConvert.SerializeObject(postModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(path, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(responseJson);
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




        public async Task<T1> PutAsync<T1,T2>(string path, T2 postModel)
        {
            await SetAuthorizedHeader();
            var res = await httpClient.PutAsJsonAsync(path, postModel);
            if (res != null && res.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync());
                return content;
            }
            return default;
        }

        public async Task<T> DeleteAsync<T>(string path)
        {
            await SetAuthorizedHeader();
            return await  httpClient.DeleteFromJsonAsync<T>(path);
        }
    }
}
