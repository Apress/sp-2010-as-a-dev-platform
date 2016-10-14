using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using System.Reflection;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties p;
            if (args.Length > 0 && !args[0].StartsWith("xml"))
            {
                p = Properties.ReadArguments<Properties>(args);
            }
            else
            {
                string[] param = args[0].Split(new char[] { ':' }, 2);
                if (param.Length == 2 && param[0].Equals("xml"))
                {
                    if (File.Exists(param[1]))
                    {

                        XDocument doc = XDocument.Load(param[1]);
                        p = Properties.Deserialize<Properties>(doc);
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                else
                {
                    throw new ArgumentException("parameter 'xml' expected");
                }
            }
            ReadFarm(p);
        }

private static void ReadFarm(Properties p)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.BufferHeight = 1000;
    SPFarm farm = SPFarm.Local;
    Console.WriteLine(farm.Id.ToString("N"));
    var srvs = from s in farm.Services where s is SPWebService select s;
    Action<object> a = v =>
        {
            PropertyInfo pi = v.GetType().GetProperty("ID");
            if (pi == null || !pi.PropertyType.Equals(typeof(Guid))) return;
            Guid id = (Guid)pi.GetValue(v, null);
            Console.WriteLine(id.ToString("N").ToUpper());
        };
    foreach (SPWebService srv in srvs)
    {
        a(srv);
        foreach (SPWebApplication webapp in srv.WebApplications)
        {
            a(webapp);
            foreach (SPSite site in webapp.Sites)
            {
                a(site);
                foreach (SPWeb web in site.AllWebs)
                {
                    a(web);
                    foreach (SPList list in web.Lists)
                    {
                        a(list);
                        foreach (SPField field in list.Fields)
                        {
                            a(field);
                        }
                    }
                    web.Dispose();
                }
                site.Dispose();
            }
        }
    }
    Console.ReadLine();
}

    }

}
