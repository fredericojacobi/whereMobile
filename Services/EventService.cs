using System.Diagnostics;
using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.Event;

namespace WhereMobile.Services;

public class EventService
{
    private readonly HttpClient _client;

    private ResponseRequestDTO<EventDTO> _responseRequest;

    public List<EventDTO> EventList = new();

    public EventService()
    {
        _client = new HttpClient();
    }
    public async Task<List<EventDTO>> GetEvents()
    {
        if (EventList.Any())
            EventList.Clear();

        var url = "https://fredericojacobi.github.io/events.json";

        try
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _responseRequest = await response.Content.ReadFromJsonAsync<ResponseRequestDTO<EventDTO>>();
                if (_responseRequest.RecordsCount > 0)
                {
                    EventList.AddRange(_responseRequest.Content.Take(4));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"########## {DateTime.Now}: Deu ruim \n {ex.Message} ##########");
        }

        return EventList;
    }

}