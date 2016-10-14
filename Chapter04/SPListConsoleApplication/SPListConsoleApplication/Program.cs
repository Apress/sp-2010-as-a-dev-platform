using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace SPListConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = (args.Length == 0) ? "3" : args[0];
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList questions = web.Lists["Questions"];
                    switch (test)
                    {
                        case "1":
                            # region 1
                            SPField field = questions.Fields["Voters"];
                            field.Hidden = false;
                            field.ReadOnlyField = false;
                            field.Update();
                            # endregion 1
                            break;
                        case "2":
                            # region 2
                            SPListItem item0 = questions.GetItemById(1);
                            if (item0.Fields["Voters"].Type == SPFieldType.User)
                            {
                                if (item0["Voters"] == null)
                                {
                                    SPFieldUserValue uv = new SPFieldUserValue(web, web.CurrentUser.ID, web.CurrentUser.Name);
                                    SPFieldUserValueCollection coll = new SPFieldUserValueCollection();
                                    coll.Add(uv);
                                    item0["Voters"] = coll;
                                }
                                else
                                {
                                    // add all users
                                    var users = from u in web.AllUsers.Cast<SPUser>()
                                                where
                                                    !u.Name.Contains(web.CurrentUser.Name)
                                                    && !u.Name.StartsWith("NT AUTHORITY")
                                                    && !u.Name.StartsWith("SHAREPOINT")
                                                    && !u.Name.EndsWith("SYSTEM")
                                                select new SPFieldUserValue(web, u.ID, u.Name);
                                    SPFieldUserValueCollection coll = (SPFieldUserValueCollection)item0["Voters"];
                                    foreach (SPFieldUserValue user in users)
                                    {
                                        coll.Add(user);
                                    }
                                    item0["Voters"] = coll;
                                }
                                item0.Update();
                            }
                            # endregion 2
                            break;
                        case "3":
                            # region 3 - Handle Huge Lists
                            // Assume a list "HugeList" - First we fill in some items
                            SPList hugeList = web.Lists["HugeList"];
                            //for (int i = 1; i < 50000; i++)
                            //{
                            //    SPListItem item = hugeList.Items.Add();
                            //    item["Title"] = String.Format("Item No {0}", i);
                            //    item.Update();
                            //}
                            //hugeList.Update();
                            SPQuery q = new SPQuery();
                            q.RowLimit = 10;
                            q.Query = "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>"; 
                            int intIndex = 1;

                            do
                            {
                                Console.WriteLine("## Page: " + intIndex);
                                SPListItemCollection listItems = hugeList.GetItems(q);

                                foreach (SPListItem listItem in listItems)
                                {
                                    Console.WriteLine(listItem["Title"].ToString());
                                }

                                q.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                                Console.WriteLine(q.ListItemCollectionPosition.PagingInfo);
                                intIndex++;
                            } while (q.ListItemCollectionPosition != null);
                            Console.ReadLine();
                            # endregion
                            break;
                    }
                }
            }
        }
    }
}
