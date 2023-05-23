using FluiReader.ViewModels;

namespace FluiReader.Views;

public partial class AddSubscriptionModalPage : ContentPage
{
    public AddSubscriptionPageViewModel ViewModel { get; set; }
    public AddSubscriptionModalPage()
	{
		InitializeComponent();
        this.ViewModel = new();
        this.BindingContext = ViewModel;
	}
}