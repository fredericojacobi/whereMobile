using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WhereMobile.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(IsNotBusy))]
    private bool _isBusy;
    
    [ObservableProperty]
    private bool _isRefreshing;

    public bool IsNotBusy => !_isBusy;

    public BaseViewModel()
    {
    }
}