using CodeHollow.FeedReader;
using CommunityToolkit.Mvvm.ComponentModel;
using FluiReader.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class FeedPageViewModel : ObservableObject
    {
        private int id;
        public int Id { get => id; set => SetProperty(ref id, value); }
        private Feed? feed;

        public Feed? Feed { get => feed; set => SetProperty(ref feed, value); }

        private readonly FeedService _feed;
        public FeedPageViewModel()
        {
            this._feed = App.Services.GetRequiredService<FeedService>();
            this.PropertyChanged += HandleIdChanged;
        }
        public async void HandleIdChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Id)) return;
            this.Feed = await _feed.LoadFeedAsync(Id);
        }
    }
}
