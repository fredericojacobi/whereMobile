using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MvvmHelpers;
using Shared.DTOs.Event;
using Shared.DTOs.UserApplication;
using WhereMobile.Components;
using WhereMobile.Services;
using WhereMobile.Views;

namespace WhereMobile.ViewModels;

public partial class EventsViewModel : BaseViewModel
{
    public ObservableRangeCollection<EventDTO> Events { get; } = new();

    private readonly EventService _eventService;

    private readonly UserService _userService;

    private IConnectivity _connectivity;
    private IGeolocation _geolocation;

    public EventsViewModel(EventService eventService, UserService userService, IConnectivity connectivity,
        IGeolocation geolocation)
    {
        Title = "Eventos";
        _eventService = eventService;
        _userService = userService;
        _connectivity = connectivity;
        _geolocation = geolocation;
        GetEventsAsync();
    }

    [ICommand]
    async Task GetClosestEventAsync()
    {
        if (IsBusy || Events.Count.Equals(0)) return;

        try
        {
            var location = await _geolocation.GetLastKnownLocationAsync();
            if (location is null)
            {
                location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30),
                });
            }

            if (location is null) return;

            var first = Events
                .OrderBy(x =>
                    location.CalculateDistance(x.Location.Latitude, x.Location.Longitude, DistanceUnits.Kilometers))
                .FirstOrDefault();

            if (first is null) return;

            await Shell.Current.DisplayAlert("Próximo a você",
                $"O evento mais próximo de você é o {first.Name}, que começa  {first.StartDate}",
                "OK");
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            await Shell.Current.DisplayAlert("Ops!", "Não foi possível buscar o evento mais próximo.", "OK");
        }
    }

    [ICommand]
    async Task GoToUserDetailsAsync()
    {
        UserApplicationDTO user = await _userService.GetUser();

        if (user == null) return;

        await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}", true, new Dictionary<string, object>
        {
            { "User", user }
        });
    }

    [ICommand]
    async Task GoToEventDetailsAsync(EventDTO eEvent)
    {
        if (eEvent is null) return;

        await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}", true, new Dictionary<string, object>
        {
            { "Event", eEvent }
        });
    }

    [ICommand]
    async Task RefreshEventsAsync() => GetEventsAsync();

    public async Task GetEventsAsync()
    {
        if (IsBusy) return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Ops!", "Você não possui uma conexão com a internet.", "OK");
            }

            IsBusy = true;
            var events = await _eventService.GetEvents();
            Events.ReplaceRange(events);
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            await Shell.Current.DisplayAlert("Ops!", "Algo deu errado.", "OK");
        }
        finally
        {
            IsRefreshing = false;
            IsBusy = false;
        }
    }
}