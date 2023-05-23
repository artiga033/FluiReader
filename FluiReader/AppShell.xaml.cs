using FluiReader.ViewModels;

namespace FluiReader;

public partial class AppShell : Shell
{
	public AppShellViewModel ViewModel { get; set; } = new();
    public AppShell()
	{
		InitializeComponent();
		this.BindingContext = this;
        Routes.InitRoutes();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
