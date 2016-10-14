using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using System.IO;
using System.Text;

namespace Apress.SP2010.Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    DatacontextDataContext ctx = new DatacontextDataContext("http://sharepointserve/");
                    goto Example6;
                    # region Example 1 - Simple
                Example1:
                    var authors = from a in ctx.Authors
                                  select a;
                    foreach (var ac in authors)
                    {

                        Console.WriteLine("{0}, {1}, {2}", ac.Company, ac.FirstName, ac.EMail);
                    }
                    goto Finish;
                    # endregion Example 1
                    # region Example 2 - Anonymous Type
                Example2:
                    var authors2 = from a in ctx.Authors
                                   select new
                                   {
                                       Firm = a.Company,
                                       Name = a.FullName,
                                       Mail = a.EMail
                                   };
                    foreach (var ac in authors2)
                    {
                        Console.WriteLine("{0}, {1}, {2}", ac.Firm, ac.Name, ac.Mail);
                    }
                    goto Finish;
                    # endregion Example 2
                    # region Example 3 - Insert Item
                Example3:
                    EntityList<AuthorsContact> list = ctx.GetList<AuthorsContact>("Authors");
                    foreach (var ac in list)
                    {
                        Console.WriteLine("{0}, {1}, {2}", ac.Company, ac.FullName, ac.EMail);
                    }
                    EntityList<AuthorsContact> list2 = ctx.GetList<AuthorsContact>("Authors");
                    AuthorsContact newAuthor = new AuthorsContact();
                    newAuthor.FirstName = "Bernd";
                    newAuthor.Title = "Pehlke";
                    newAuthor.EMail = "bpehlke@computacenter.com";
                    newAuthor.Company = "Computacenter";
                    list2.InsertOnSubmit(newAuthor);
                    ctx.SubmitChanges();
                    foreach (var ac in list2)
                    {
                        Console.WriteLine("{0}, {1}, {2}", ac.Company, ac.FullName, ac.EMail);
                    }
                    goto Finish;
                    # endregion
                    # region Example 4 - Deleting
                Example4:
                    var authors3 = from a in ctx.Authors
                                   where a.Title.Equals("Pehlke")
                                   select a;
                    foreach (var ac in authors3)
                    {
                        ctx.Authors.DeleteOnSubmit(ac);
                        Console.WriteLine("Delete: {0}, {1}, {2}", ac.Company, ac.FullName, ac.EMail);
                    }
                    ctx.SubmitChanges();

                    goto Finish;
                    # endregion Example 4
                    # region Example 5 - Read Joined List
                Example5:
                    EntityList<AuthorsContact> authorsj = ctx.GetList<AuthorsContact>("Authors");
                    EntityList<BooksItem> booksj = ctx.GetList<BooksItem>("Books");
                    var result5 = from book in booksj
                                  join author in authorsj on book.LeadAuthor.Id equals author.Id
                                  select new
                                  {
                                      Book = book.Title,
                                      Author = author.FullName
                                  };
                    result5.ToList().ForEach(ab => Console.WriteLine("{0} was written by {1}",
                        ab.Book,
                        ab.Author));
                    goto Finish;
                    # endregion Example 5 - Read Joined List
                    # region Example 6 - Logging
                Example6:
                    StringBuilder sb = new StringBuilder();
                    TextWriter tw = new StringWriter(sb);
                    ctx.Log = tw;
                    EntityList<AuthorsContact> authorsj2 = ctx.GetList<AuthorsContact>("Authors");
                    EntityList<BooksItem> booksj2 = ctx.GetList<BooksItem>("Books");
                    var result6 = from book in booksj2
                                  join author in authorsj2 on book.LeadAuthor.Id equals author.Id
                                  select new
                                  {
                                      Book = book.Title,
                                      Author = author.FullName
                                  };
                    result6.ToList().ForEach(ab => Console.WriteLine("{0} was written by {1}",
                        ab.Book,
                        ab.Author));
                    Console.WriteLine(sb.ToString());
                    tw.Dispose();
                    goto Finish;
                    # endregion Example 6 - Logging

                Finish:
                    Console.ReadLine();
                }
            }
        }
    }
}
