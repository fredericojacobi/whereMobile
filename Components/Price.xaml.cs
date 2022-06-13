using Microsoft.Maui.Handlers;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WhereMobile.Components;

public partial class Price
{
    public static readonly BindableProperty PriceRateProperty =
        BindableProperty.Create("PriceRate", typeof(int), typeof(PriceCustom), 1);

    public int PriceRate
    {
        get => (int)GetValue(PriceRateProperty);
        set => SetValue(PriceRateProperty, value);
    }

    public Price()
    {
        InitializeComponent();
/*
        var priceEnabled = new Image
        {
            Source = "~/Images/dollar.svg"
        };

        var priceUnabled = new Image
        {
            Source = "~/Images/dollardisabled.svg"
        };

        var horizontalLayout = new HorizontalStackLayout();
        Children.Add(priceUnabled);
        Children.Add(priceUnabled);
        Children.Add(priceUnabled);
        Children.Add(priceUnabled);
        Children.Add(priceUnabled);
        for (int i = 0; i < PriceRate; i++)
        {
            Children.Insert(i, priceEnabled);
 
        }
*/
    }
}