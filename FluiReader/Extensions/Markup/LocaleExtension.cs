using FluiReader.Services;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Extensions.Markup
{
    public class LocaleExtension : IMarkupExtension<string>
    {
        private readonly LocaleService _locale;
        public LocaleExtension()
        {
            _locale = App.Services.GetRequiredService<LocaleService>();
        }

        public LocaleKeys Key { get; set; }
        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return _locale.GetLocale(Key);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}
