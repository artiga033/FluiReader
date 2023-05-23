using CodeHollow.FeedReader;
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
    public class LocalFeedService
    {
        private readonly HttpClient _httpClient;

        public LocalFeedService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        
    }
}
