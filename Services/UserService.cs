using System.Diagnostics;
using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.UserApplication;

namespace WhereMobile.Services;

public class UserService
{
    private readonly HttpClient _client;

    private ResponseRequestDTO<UserApplicationDTO> _responseRequest;

    public UserApplicationDTO User = new();

    public UserService()
    {
        _client = new HttpClient();
    }
    public async Task<UserApplicationDTO> GetUser()
    {
        if (User is not null)
            User = new UserApplicationDTO();

        var url = "https://fredericojacobi.github.io/users.json";

        try
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _responseRequest = await response.Content.ReadFromJsonAsync<ResponseRequestDTO<UserApplicationDTO>>();
                if (_responseRequest.RecordsCount > 0)
                {
                    User = _responseRequest.Content.FirstOrDefault();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"########## {DateTime.Now}: Deu ruim \n {ex.Message} ##########");
        }

        return User;
    }
}