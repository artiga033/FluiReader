using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluiReader.Extensions
{
    public static class UriExtensions
    {
        public static string ToSafeString(this Uri uri) =>
            uri.Host + uri.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped).Replace("/", "_").Replace("?", "-");
    }
}
