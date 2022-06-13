using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Shared.DTOs.Event;

namespace WhereMobile.ViewModels;

[QueryProperty("Event", "Event")]
public partial class EventDetailsViewModel : BaseViewModel
{
    private IMap _map;

    public EventDetailsViewModel(IMap map)
    {
        _map = map;
    }

    [ObservableProperty]
    private EventDTO _event;

    [ICommand]
    async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Event.Location.Latitude, Event.Location.Longitude, new MapLaunchOptions
            {
                Name = Event.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            await Shell.Current.DisplayAlert("Ops!", "Não foi possível abrir o mapa.", "OK");
        }
    }
}