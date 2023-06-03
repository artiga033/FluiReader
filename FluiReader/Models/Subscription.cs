using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using FluiReader.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Models
{
    /// <summary>
    /// A RSS Subscription
    /// </summary>
    public class Subscription
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// Type of the subscription source
        /// </summary>
        public FeedType Type { get; set; } = FeedType.Unknown;
        /// <summary>
        /// The subscription URI
        /// </summary>
        public Uri? Link { get; set; }
        /// <summary>
        /// The DateTime when the last time this subscription has been checked
        /// </summary>
        public DateTime LastCheckedUpdate { get; set; }
        /// <summary>
        /// The =tile of the subscription, may be manually set by the user and different from the feed's.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The Favicon of the feed. Usually same as the site's
        /// </summary>
        public Uri? FavIcon { get; set; }

    }
    public static class SubscriptionModelEx
    {
        public static async Task CheckForUpdateAsync(this Subscription sub, HttpClient _http)
        {
            if (sub.Link is null) throw new InvalidOperationException("Subscription url is null");
            var res = await _http.GetStringAsync(sub.Link);
            File.WriteAllText(Path.Combine(Constants.FeedCacheDir, sub.Link.ToSafeString()), res);
            var feed = FeedReader.ReadFromString(res);
            sub.Title = feed.Title;
            sub.Type = feed.Type;
            sub.LastCheckedUpdate = DateTime.Now;
            sub.FavIcon = new UriBuilder(sub.Link)
            {
                Path = "/favicon.ico"
            }.Uri;
        }
    }
}
