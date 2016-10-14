using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace CreateListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = null;
                    string listName = "NewBooks";
                    // check whether the list already exists
                    try
                    {
                        list = web.Lists[listName];
                    }
                    catch (ArgumentException)
                    {
                    }
                    if (list == null)
                    {
                        Guid listId = web.Lists.Add(listName, "All our books",
                                      SPListTemplateType.GenericList);
                        list = web.Lists[listId];
                        list.OnQuickLaunch = true;
                        list.Update();
                    }
                    Console.WriteLine("Created list {0} with id {1}", list.Title, list.ID);
                }
            }

        }
    }
}
