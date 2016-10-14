using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace CreateListConfigured
{
    class Program
    {
        static void Main(string[] args)
        {
            string lookupFieldName = "RelatedField";
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPListCollection lists = web.Lists;
                    Guid SourceListId = lists.Add("Parent List",
                        "",
                        SPListTemplateType.GenericList);
                    Console.WriteLine("Parent List Done...");
                    Guid TargetListId = lists.Add("Child List",
                        "",
                        SPListTemplateType.GenericList);
                    Console.WriteLine("Child List Done...");
                    SPList SourceList = lists[SourceListId];
                    SPList TargetList = lists[TargetListId];
                    SPFieldCollection Fields = TargetList.Fields;
                    Fields.AddLookup(lookupFieldName, SourceList.ID, true);
                    Console.WriteLine("Lookup Field Created");
                    SPFieldLookup NewLookupField = Fields[lookupFieldName] as SPFieldLookup;
                    NewLookupField.Indexed = true;
                    NewLookupField.LookupField = "Title";
                    NewLookupField.RelationshipDeleteBehavior = SPRelationshipDeleteBehavior.Restrict;
                    NewLookupField.Update();
                    Console.WriteLine("Lookup field integrity enforced");
                    SPListItem NewSourceItem = SourceList.Items.Add();
                    NewSourceItem["Title"] = "Parent Data";
                    NewSourceItem.Update();
                    Console.WriteLine("Source listitem created");
                    SPListItem NewTargetItem = TargetList.Items.Add();
                    NewTargetItem["Title"] = "Child Data";
                    NewTargetItem[lookupFieldName] = new SPFieldLookupValue(1, "Source Data");
                    NewTargetItem.Update();
                    Console.WriteLine("Parent listitem created");
                    TargetList.Update();
                    SourceList.Update();
                }
            }
            Console.ReadLine();
        }
    }
}
