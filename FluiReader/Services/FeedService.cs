using CodeHollow.FeedReader;
using FluiReader.Extensions;
using FluiReader.Models;
using FluiReader.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Services
{
    /// <summary>
    /// fetches feeds from by the local client itself
    /// </summary>
    public class FeedService
    {
        private readonly HttpClient _httpClient;
        private readonly ISubscriptionRepoService _sub;

        public FeedService(HttpClient httpClient, ISubscriptionRepoService sub)
        {
            this._httpClient = httpClient;
            this._sub = sub;
        }

        public async Task<Feed> LoadFeedAsync(Subscription sub)
        {
            var cachePath = Path.Combine(Constants.FeedCacheDir, sub.Link!.ToSafeString());
            var lastWriteTime = File.GetLastWriteTime(cachePath);
            if (DateTime.Now - lastWriteTime > TimeSpan.FromMinutes(15))
                await sub.CheckForUpdateAsync(_httpClient);
            var feed = await FeedReader.ReadFromFileAsync(cachePath);
            return feed;
        }
        public async Task<Feed> LoadFeedAsync(int subscriptionId) => await LoadFeedAsync(await _sub.GetSubscriptionAsync(subscriptionId));
    }
}
