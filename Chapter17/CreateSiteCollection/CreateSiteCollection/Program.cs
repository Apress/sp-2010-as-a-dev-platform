using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Xml.Linq;

namespace Apress.SP2010.CreateSiteCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Environment.ExitCode = CreateSiteCollection();
        }

        private static int CreateSiteCollection()
        {
            try
            {
                string srvUrl = "http://sharepointserve";
                using (SPSite site = new SPSite(srvUrl))
                {
                    // current collection
                    SPSiteCollection coll = site.WebApplication.Sites;
                    XDocument definition = XDocument.Load("SiteDefinition.xml");
                    XElement root = definition.Element("SiteDefinition");
                    SPSite newSite = coll.Add(
                        root.Element("Url").Value,
                        root.Element("Title").Value,
                        root.Element("Description").Value,
                        Convert.ToUInt32(root.Element("LCID").Value),
                        (String.IsNullOrEmpty(root.Element("WebTemplate").Value) ? null : root.Element("WebTemplate").Value),
                        root.Element("OwnerLogin").Value,
                        root.Element("OwnerName").Value,
                        root.Element("OwnerEmail").Value
                        );
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
