using CodeHollow.FeedReader;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    [QueryProperty(nameof(Item),"item")]
    public class FeedItemPageViewModel : ObservableObject
    {
        private FeedItem? item;
        public FeedItem? Item
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }

        public FeedItemPageViewModel()
        {
            
        }
    }
}
