using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Xml.Linq;

namespace RetrieveExternalDataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists["NorthwindProducts"];
                        //SPQuery query = new SPQuery();
                        //var xml = new XElement("Where",
                        //            new XElement("Equals",
                        //                new XElement("FieldRef", new XAttribute("Name", "CategoryID")),
                        //                new XElement("Value", 1)));

                        //query.Query = xml.ToString();
                        //SPListItemCollection items = list.GetItems(query);
                         foreach (SPListItem item in list.Items)
                        {
                            Console.WriteLine(item.DisplayName);
                        }
                    }
                }
                );
            }
            Console.ReadLine();
        }
    }
}
