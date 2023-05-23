using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiReader.Models;
using FluiReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    public partial class SubscriptionPageViewModel : ObservableObject
    {
        public SubscriptionPageViewModel()
        {
            this._sub = App.Services.GetRequiredService<ISubscriptionRepoService>();
            this._http = App.Services.GetRequiredService<HttpClient>();
        }
        private ObservableCollection<SubscriptionViewModel> subscriptions = new();
        private ISubscriptionRepoService _sub;
        private readonly HttpClient _http;
        public ObservableCollection<SubscriptionViewModel> Subscriptions
        {
            get => subscriptions;
            set => SetProperty(ref subscriptions, value);
        }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        [RelayCommand]
        public async Task LoadDataAsync()
        {
            var data = await this._sub.GetSubscriptionAsync(Page++, PageSize);
            foreach (var i in data)
            {
                this.subscriptions.Add(new(i, _http));
            }
        }
        [RelayCommand]
        public void NavigateToAddPage()
        {
            Shell.Current.GoToAsync(Routes.SUBSCRIPTION_ADD_PAGE);
        }
    }
    public partial class SubscriptionViewModel : ObservableObject
    {
        private readonly HttpClient _http;
        public Subscription Subscription { get; set; }
        [RelayCommand]
        public async void CheckUpdate()
        {
            await this.Subscription.CheckForUpdateAsync(_http);
            OnPropertyChanged(nameof(Subscription));
        }
        public SubscriptionViewModel(Subscription subscription, HttpClient http)
        {
            this.Subscription = subscription;
            this._http = http;
        }
    }
}
