using Shared.DTOs.UserApplication;
using WhereMobile.Services;
using WhereMobile.ViewModels;

namespace WhereMobile.Views;

public partial class UserDetailsPage : ContentPage
{

    public UserDetailsPage(UserDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}