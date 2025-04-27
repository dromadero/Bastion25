using System.Buffers.Text;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace Bastion.Common;

public class AppLoger
{
    string _appId = string.Empty;
    HttpClient? _httpClient = null;

    public AppLoger(string appId, HttpClient httpClient, string baseApiUrl)
    {
        _appId = appId;
        Configure(httpClient, baseApiUrl);
    }


    private void Configure(HttpClient httpClient, string url)
    {
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(20);
        _httpClient.BaseAddress = new Uri(url);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<bool> Log(string message)
    {
        var queryParams = new Dictionary<string, string?>
            {
              //  { "appId", _appId }
            };

        string ApiURL_WithQuery = QueryHelpers.AddQueryString($"applogs", queryParams);

        if (_httpClient is not null && !string.IsNullOrWhiteSpace(message))
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiURL_WithQuery, new { BusinessId = _appId, Message = message });

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
           
        }
        return false;
    }

}
