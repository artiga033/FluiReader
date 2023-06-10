using CodeHollow.FeedReader;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiReader.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class FeedPageViewModel : ObservableObject
    {
        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }
        private Feed? feed;

        public Feed? Feed
        {
            get => feed; set
            {
                SetProperty(ref feed, value);
                OnPropertyChanged(nameof(ItemViewModels));
            }
        }

        private readonly FeedService _feed;
        public ObservableCollection<FeedItemViewModel> ItemViewModels { get; } = new();
        public FeedPageViewModel()
        {
            this._feed = App.Services.GetRequiredService<FeedService>();
            this.PropertyChanged += HandleIdChanged;

        }
        public async void HandleIdChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Id)) return;
            // this is actually huge compute, do not do it on UI thread.
            await Task.Run(async () =>
            {
                this.Feed = await _feed.LoadFeedAsync(Id);
                if (feed != null)
                {
                    foreach (var item in feed.Items)
                        ItemViewModels.Add(new FeedItemViewModel(item));
                }
            });
        }

    }
    public partial class FeedItemViewModel : ObservableObject
    {
        public FeedItem Item { get; set; }

        public FeedItemViewModel(FeedItem item)
        {
            Item = item;
        }

        [RelayCommand]
        public async void NavigateToItemPage()
        {
            var args = new Dictionary<string, object>() { { "item", Item } };
            await Shell.Current.GoToAsync($"{Routes.FEED_ITEM_PAGE}", args);
        }
    }
}
