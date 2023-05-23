using FluiReader.Extensions;
using FluiReader.Resources.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Services
{
    public class LocaleService
    {
        public LocaleService()
        {
        }
        public string GetLocale(string key) => AppStrings.ResourceManager.GetString(key) ?? $"%{key}%";
        public string GetLocale(LocaleKeys key) => GetLocale(key.ToString());
    }
}
