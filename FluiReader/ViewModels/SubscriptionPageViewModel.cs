using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiReader.Models;
using FluiReader.Services;
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
        private readonly ISubscriptionRepoService _sub;
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
                this.subscriptions.Add(new(i, _http, _sub));
            }
        }
        [RelayCommand]
        public async Task ReloadAsync()
        {
            this.Subscriptions = new();
            this.Page = 1;
            await LoadDataAsync();
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
        private readonly ISubscriptionRepoService _sub;

        public Subscription Subscription { get; set; }
        [RelayCommand]
        public async void CheckUpdate()
        {
            await this.Subscription.CheckForUpdateAsync(_http);
            await _sub.EditSubscriptionAsync(this.Subscription);
            OnPropertyChanged(nameof(Subscription));
        }
        [RelayCommand]
        public async void NavigateToFeedPage()
        {
            await Shell.Current.GoToAsync($"{Routes.FEED_DETAIL_PAGE}?id={Subscription.Id}");
        }
        public SubscriptionViewModel(Subscription subscription, HttpClient http, ISubscriptionRepoService sub)
        {
            this.Subscription = subscription;
            this._http = http;
            this._sub = sub;
        }
    }
}
