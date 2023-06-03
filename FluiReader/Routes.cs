using FluiReader.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader
{
    public static class Routes
    {
        public static readonly string SUBSCRIPTION_ADD_PAGE = "subscriptions/add";
        public static readonly string FEED_DETAIL_PAGE = "feeddetail";
        public static void InitRoutes()
        {
            Routing.RegisterRoute(SUBSCRIPTION_ADD_PAGE, typeof(AddSubscriptionModalPage));
            Routing.RegisterRoute(FEED_DETAIL_PAGE, typeof(FeedPage));
        }
    }
}
