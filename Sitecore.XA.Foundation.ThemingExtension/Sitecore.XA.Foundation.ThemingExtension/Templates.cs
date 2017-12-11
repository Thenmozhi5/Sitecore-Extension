using Sitecore.Data;

namespace Sitecore.XA.Foundation.ThemingExtension
{
    public class Templates
    {
        public struct AssetLink
        {
            public static ID Id = ID.Parse("{3CFCD30C-AF90-4163-BDAA-3B937E56E94A}");
            public struct Fields
            {
                public static readonly ID ScriptUrls = new ID("{3C4E21CA-79A7-4A6B-A82C-2B9E4E502BC5}");
                public static readonly ID StyleUrls = new ID("{09B8805C-B108-4148-AD2F-1E87D8E69F4D}");
            }
        }
    }
}