using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.Web.Administration;
using System.Web;

namespace ResourceBasedVisualWebPart.Features.WebPartVPPFeature
{

    [Guid("84e28e75-4987-4760-8083-81cd97851871")]
    public class WebPartVPPFeatureEventReceiver : SPFeatureReceiver
    {
        const string VPPRegisterHandler = "VPPRegisterHandler";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            // Add Handler to web.config to force VPP registering
            SPSite site = properties.Feature.Parent as SPSite;
            if (site != null)
            {
                using (ServerManager manager = new ServerManager())
                {
                    try
                    {
                        Configuration webConfig = manager.GetWebConfiguration("SharePoint - 80");
                        ConfigurationSection modules = webConfig.GetSection("system.webServer/modules");
                        ConfigurationElementCollection modulesCollection = modules.GetCollection();
                        ConfigurationElement moduleElement = modulesCollection.CreateElement("add");
                        moduleElement["name"] = VPPRegisterHandler;
                        moduleElement["type"] = typeof(WebPartVPPRegModule).AssemblyQualifiedName;
                        modulesCollection.Add(moduleElement);
                        manager.CommitChanges();
                    }
                    catch
                    {
                    }
                }

            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;
            if (site != null)
            {
                using (ServerManager manager = new ServerManager())
                {
                    try
                    {
                        Configuration webConfig = manager.GetWebConfiguration("SharePoint - 80");
                        ConfigurationSection modules = webConfig.GetSection("system.webServer/modules");
                        ConfigurationElementCollection modulesCollection = modules.GetCollection();
                        foreach (ConfigurationElement module in modulesCollection)
                        {
                            if (String.Equals((string)module.GetAttributeValue("name"), VPPRegisterHandler, StringComparison.OrdinalIgnoreCase))
                            {
                                modulesCollection.Remove(module);
                                break;
                            }
                        }
                        manager.CommitChanges();
                    }
                    catch
                    {
                    }
                }
            }
        }

    }
}