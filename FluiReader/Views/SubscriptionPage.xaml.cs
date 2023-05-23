using FluiReader.Models;
using FluiReader.Services.Interfaces;
using FluiReader.ViewModels;

namespace FluiReader.Views;

public partial class SubscriptionPage : ContentPage
{

    public SubscriptionPageViewModel ViewModel { get; set; }
    //public ICollection<Subscription> ItemSource
    //{
    //    get { return (ICollection<Subscription>)GetValue(ItemSourceProperty); }
    //    set { SetValue(ItemSourceProperty, value); }
    //}
    //public static readonly BindableProperty ItemSourceProperty =
    //    BindableProperty.Create("ItemSource", typeof(ICollection<Subscription>), typeof(SubscriptionPage), Array.Empty<Subscription>());

    public SubscriptionPage()
    {
        InitializeComponent();
        this.ViewModel = new();
        this.BindingContext = ViewModel;
    }
    protected async override void OnAppearing()
    {
        await this.ViewModel.LoadDataAsync();
    }
}