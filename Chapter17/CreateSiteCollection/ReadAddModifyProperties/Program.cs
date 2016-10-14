using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Collections;

namespace Apress.SP2010.ReadAddModifyProperties
{
    class Program
    {

        const string PREFIX = "myprop_";

        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve/sites/BlankInternetSite/"))
            {
                using (SPWeb web = site.RootWeb)
                {
                    foreach (DictionaryEntry entry in web.Properties)
                    {
                        Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
                    }
                    string key = PREFIX + "AutoCreator";
                    if (!web.Properties.ContainsKey(key))
                    {
                        web.Properties.Add(key,
                            String.Format("Created by {0} at {1}",
                            Environment.UserName,
                            DateTime.Now));
                        web.AllowUnsafeUpdates = true;
                        web.Properties.Update();
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
