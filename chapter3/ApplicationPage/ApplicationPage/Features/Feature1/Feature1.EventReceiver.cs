using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Navigation;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Xml;
using Microsoft.SharePoint.Utilities;

namespace ApplicationPage.Features.Feature1
{
    [Guid("9f3e821a-1e27-425f-b3f6-93a2847f6544")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {

        string PATH = @"/_layouts/";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            if (properties.Feature.Parent is SPSite)
            {
                // cannot activate this feature on site level
                return;
            }
            SPWeb web = (SPWeb)properties.Feature.Parent;
            SPNavigationNodeCollection topNavi = web.Navigation.TopNavigationBar;
            // Check existing top element. If present remove first
            CheckAndRemove(topNavi);
            // Read navigation instruction
            XNamespace siteNM = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0";
            using (Stream st = GetType().Assembly.GetManifestResourceStream("ApplicationPage.web.sitemap"))
            {
                using (XmlReader tr = new XmlTextReader(st))
                {
                    try
                    {
                        XElement siteMap = XElement.Load(tr);
                        // add nodes
                        var root = from r in siteMap.Descendants()      
                                   where r.Attribute("title").Value.Equals("HR Department")
                                   select r;
                        // Found
                        if (root.Count() == 1)
                        {
                            XElement rootElement = root.First();
                            string rootPath = web.Url + PATH;
                            // create and add rootnode
                            SPNavigationNode rootNode = new SPNavigationNode(
                                rootElement.Attribute("title").Value,
                                rootPath + rootElement.Attribute("url").Value,
                                true);
                            SPNavigationNode topNode = topNavi.AddAsLast(rootNode);
                            AddNodes(rootElement, topNode, rootPath);

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void AddNodes(XElement currentFrom, SPNavigationNode currentTo, string rootPath)
        {
            foreach (XElement r in currentFrom.Elements())
            {
                SPNavigationNode n = new SPNavigationNode(
                           r.Attribute("title").Value,
                           rootPath + r.Attribute("url").Value);
                SPNavigationNode newnode = currentTo.Children.AddAsLast(n);
                if (r.HasElements)
                {
                    AddNodes(r, newnode, rootPath);
                }
            }
        }

        private void CheckAndRemove(SPNavigationNodeCollection topNavi)
        {
            var nodes = from n in topNavi.Cast<SPNavigationNode>()
                        where n.Title.Equals("HR Department")
                        select n;
            if (nodes.Count() == 1)
            {
                topNavi.Delete(nodes.First());
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWeb web = (SPWeb)properties.Feature.Parent;
            SPNavigationNodeCollection topNavi = web.Navigation.TopNavigationBar;
            CheckAndRemove(topNavi);
        }

    }
}
