using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Shared.DTOs.UserApplication;
using WhereMobile.Services;

namespace WhereMobile.ViewModels;

[QueryProperty("User", "User")]
public partial class UserDetailsViewModel : BaseViewModel
{
    private readonly UserService _userService;

    public UserDetailsViewModel(UserService userService)
    {
        Title = "Perfil";
        _userService = userService;
        GetUserAsync();

    }

    [ObservableProperty] 
    private UserApplicationDTO _user;

    private async Task GetUserAsync()
    {
        IsBusy = true;
        if (User is not null)
            User = new();

        try
        {
            User = await _userService.GetUser();
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