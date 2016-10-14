using System;
using System.Linq;
using System.Net;
using Apress.SP2010.ListService.MyServiceReference;

namespace Apress.SP2010.ListService
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://sharepointserve/_vti_bin/listdata.svc", UriKind.Absolute);
            HomeDataContext ctx = new HomeDataContext(uri);
            ctx.Credentials = new NetworkCredential("Administrator", "sharepoint?2010");

            # region Example 1
            
            var authors = from a in ctx.Authors
                          select a;

            foreach (var ac in authors)
            {
                Console.WriteLine("{0} works at {1}",
                                    ac.FullName,
                                    ac.Company);
            }
            # endregion



            Console.ReadLine();


        }
    }
}
