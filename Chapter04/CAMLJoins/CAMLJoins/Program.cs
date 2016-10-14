using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Xml;

namespace Apress.SP2010.CAMLJoins
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList bookList = web.Lists["Books"];
                    SPList authList = web.Lists["Authors"];
                    SPField la = bookList.Fields["LeadAuthor"];
                    SPField fa = authList.Fields["Full Name"];
                    string join = @"<Join Type='LEFT' ListAlias='Authors'>
                                         <Eq>
                                           <FieldRef Name='" + la.InternalName + @"' RefType='ID' />
                                           <FieldRef List='Authors' Name='ID' />
                                         </Eq>
                                       </Join>";
                    string pfld = @"<Field Name='Fullname' Type='Lookup' List='Authors' ShowField='" + fa.InternalName + "' />";
                    SPQuery query = new SPQuery();
                    query.Query = "";
                    query.Joins = join;
                    query.ProjectedFields = pfld;
                    query.ViewFields = @"<FieldRef Name='Fullname' /><FieldRef Name='Title' />";
                    SPListItemCollection items = bookList.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        Console.WriteLine("{0} has these lead authors: ", item.Title);
                        if (item["Fullname"] == null)
                        {
                            Console.WriteLine("  no authors assigned");
                            continue;
                        }
                        SPFieldLookupValue sc = new SPFieldLookupValue(item["Fullname"].ToString());
                        Console.WriteLine("  - {0}", sc.LookupValue);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
