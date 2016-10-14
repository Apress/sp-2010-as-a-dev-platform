using System.Collections.Specialized;
using System.Xml.Linq;
using Microsoft.SharePoint;

namespace CreateNewView
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList books = web.Lists["Books"];
                    StringCollection fields = new StringCollection();
                    fields.Add("Title");
                    fields.Add("Publisher");
                    fields.Add("Author");
                    var query = new XElement("Where",
                                    new XElement("Eq",
                                        new XElement("FieldRef", new XAttribute("Name", "Publisher")),
                                        new XElement("Value", new XAttribute("Type", "CHOICE"), "Apress")
                                        )
                                    ).ToString(SaveOptions.DisableFormatting);
                    SPView view = books.Views.Add("ApressBooks",
                        fields,
                        query,
                        100,
                        false,
                        false,
                        Microsoft.SharePoint.SPViewCollection.SPViewType.Html,
                        false
                        );
                }
            }

        }

    }
}
