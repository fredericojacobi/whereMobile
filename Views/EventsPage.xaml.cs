using WhereMobile.ViewModels;

namespace WhereMobile.Views;

public partial class EventsPage : ContentPage
{

	public EventsPage(EventsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}

