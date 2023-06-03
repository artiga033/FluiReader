using FluiReader.ViewModels;

namespace FluiReader.Views;

public partial class FeedItemPage : ContentPage
{
    public FeedItemPageViewModel ViewModel { get; set; } = new();
    public FeedItemPage()
    {
        InitializeComponent();
        this.BindingContext = ViewModel;
    }
}