using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Extensions
{
    public static class ExtractPublicIdOfImageFromCloudinaryURL
    {
        public static string ExtractPublicIdOfImage(this string str)
        {
            return Path.GetFileNameWithoutExtension(str.Substring(str.LastIndexOf("/")+1));
        }
    }
}
