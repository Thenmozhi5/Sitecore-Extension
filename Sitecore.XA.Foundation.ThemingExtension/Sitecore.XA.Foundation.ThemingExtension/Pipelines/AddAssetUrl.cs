//using Microsoft.Extensions.DependencyInjection;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Theming.Configuration;
using Sitecore.XA.Foundation.Theming.Pipelines.AssetService;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.XA.Foundation.ThemingExtension.Pipelines
{
    public class AddAssetUrl : AddAssetsProcessor
    {
        public override void Process(AssetsArgs args)
        {
            if (!IsSxaPage()) return;
            var assetConfiguration = AssetConfigurationReader.Read();

            var contextItem = Context.Item;
            if (contextItem == null) return;
            IMultisiteContext multiSiteContext = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(IMultisiteContext)) as IMultisiteContext;
            var siteItem = multiSiteContext.GetSiteItem(contextItem);
            //var siteItem = ServiceLocator.Current.Resolve<IMultisiteContext>().GetSiteItem(contextItem);

            var settingsItem = siteItem?.Children["Settings"];
            if (settingsItem == null) return;

            var scriptUrls = new List<string>();
            var styleUrls = new List<string>();
            var siteScriptUrls = settingsItem[Templates.AssetLink.Fields.ScriptUrls]?.Split('|');
            var pageScriptUrls = contextItem[Templates.AssetLink.Fields.ScriptUrls]?.Split('|');
            var siteStyleUrls = settingsItem[Templates.AssetLink.Fields.StyleUrls]?.Split('|');
            var pageStyleUrls = contextItem[Templates.AssetLink.Fields.StyleUrls]?.Split('|');
            scriptUrls = scriptUrls.Where(x => !string.IsNullOrEmpty(x)).Union(siteScriptUrls).ToList();
            scriptUrls = scriptUrls.Where(x => !string.IsNullOrEmpty(x)).Union(pageScriptUrls).ToList();
            styleUrls = styleUrls.Union(siteStyleUrls).ToList();
            styleUrls = styleUrls.Union(pageStyleUrls).ToList();
            scriptUrls = scriptUrls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            styleUrls = styleUrls.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if ((scriptUrls == null || !scriptUrls.Any()) && (styleUrls == null || !styleUrls.Any()))
                return;

            List<AssetInclude> assetsList = args.AssetsList;

            foreach (var styleUrl in styleUrls)
            {
                assetsList.Add(new UrlInclude()
                {
                    Url = styleUrl,
                    Type = AssetType.Style
                });
            }

            foreach (var scriptUrl in scriptUrls)
            {
                assetsList.Add(new UrlInclude()
                {
                    Url = scriptUrl,
                    Type = AssetType.Script
                });
            }
        }

    }
}