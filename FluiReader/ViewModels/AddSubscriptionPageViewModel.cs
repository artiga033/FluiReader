using CodeHollow.FeedReader;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiReader.Extensions;
using FluiReader.Models;
using FluiReader.Services;
using FluiReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    public partial class AddSubscriptionPageViewModel : ObservableObject
    {
        private readonly HttpClient _http;
        private readonly ISubscriptionRepoService _sub;
        public AddSubscriptionPageViewModel()
        {
            this._http = App.Services.GetRequiredService<HttpClient>();
            this._sub = App.Services.GetRequiredService<ISubscriptionRepoService>();
            this.PropertyChanged += LinkTextChangedHandler;
        }

        private Uri? link;
        private bool linkIsValid;
        private bool validatingLink;

        public Subscription Entity { get; set; } = new();
        public string Link
        {
            get => link?.ToString() ?? ""; set
            {
                if (Uri.TryCreate(value, new(), out Uri? res))
                {
                    SetProperty(ref link, res);
                }
            }
        }
        public bool LinkIsValid
        {
            get => linkIsValid; set
            {
                SetProperty(ref linkIsValid, value);
                OnPropertyChanged(nameof(LinkValidStatus));
                OnPropertyChanged(nameof(LinkValidIcon));
            }
        }
        public FluentSystemIconsRegularKeys LinkValidIcon => validatingLink ? FluentSystemIconsRegularKeys.Arrow_rotate_clockwise_24_regular : linkIsValid ? FluentSystemIconsRegularKeys.Checkmark_24_regular : FluentSystemIconsRegularKeys.Document_error_24_regular;
        public string LinkValidStatus => validatingLink ? "validating..." : linkIsValid ? "valid" : "invalid";
        [RelayCommand]
        public async Task LoadFeedInfo()
        {
            OnPropertyChanging(nameof(Entity));
            this.Entity.Link = link;
            await this.Entity.CheckForUpdateAsync(_http);
            OnPropertyChanged(nameof(Entity));
        }

        public bool ValidatingLink { get => validatingLink; set => SetProperty(ref validatingLink, value); }

        public async void LinkTextChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Link))
                return;

            OnPropertyChanging(nameof(LinkValidStatus));
            OnPropertyChanging(nameof(LinkValidIcon));
            if (validatingLink) return;
            await Task.Delay(800);
            if (validatingLink) return;
            ValidatingLink = true;
            OnPropertyChanged(nameof(LinkValidStatus));
            OnPropertyChanged(nameof(LinkValidIcon));
            try
            {
                await LoadFeedInfo();
                this.LinkIsValid = true;
            }
            catch (Exception)
            {
                this.LinkIsValid = false;
            }
            ValidatingLink = false;
            OnPropertyChanged(nameof(LinkValidStatus));
            OnPropertyChanged(nameof(LinkValidIcon));
        }

        [RelayCommand]
        public async Task Save()
        {
            await _sub.AddSubscriptionAsync(this.Entity);
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
