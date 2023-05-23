using CommunityToolkit.Mvvm.ComponentModel;
using FluiReader.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.ViewModels
{
    public class AppShellViewModel : ObservableObject
    {
        public ObservableCollection<FlyoutItem> FlyoutItems { get; set; } = new ObservableCollection<FlyoutItem>() { };
        public AppShellViewModel()
        {
            var testItem = new FlyoutItem
            {
                Title = "Fast"
            };
            testItem.Items.Add(new ShellContent() { ContentTemplate = new DataTemplate(typeof(MainPage)),Route="MainPage" });
            this.FlyoutItems.Add(testItem);
        }
    }
}
