using FluiReader.ViewModels;

namespace FluiReader.Views;


public partial class FeedPage : ContentPage
{
    public FeedPageViewModel ViewModel { get; set; } = new();
    public FeedPage()
    {
        InitializeComponent();
        this.BindingContext = ViewModel;
    }
}