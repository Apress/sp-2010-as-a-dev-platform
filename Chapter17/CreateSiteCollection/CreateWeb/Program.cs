using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Apress.SP2010.CreateWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Environment.ExitCode = CreateWeb();
        }

        private static int CreateWeb()
        {
            try
            {
                string srvUrl = "http://sharepointserve/sites/BlankInternetSite";
                using (SPSite site = new SPSite(srvUrl))
                {
                    // current collection
                    XDocument definition = XDocument.Load("WebDefinition.xml");
                    XElement root = definition.Element("SiteDefinition");
                    SPWeb newWeb = site.AllWebs.Add(
                        root.Element("Url").Value,
                        root.Element("Title").Value,
                        root.Element("Description").Value,
                        Convert.ToUInt32(root.Element("LCID").Value),
                        (String.IsNullOrEmpty(root.Element("WebTemplate").Value) ? null : root.Element("WebTemplate").Value),
                        Boolean.Parse(root.Element("UniquePermissions").Value),
                        Boolean.Parse(root.Element("ConvertIfThere").Value)
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
