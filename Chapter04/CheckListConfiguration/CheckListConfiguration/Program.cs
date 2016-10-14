using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Apress.SP2010.CheckListConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {

                    SPRelatedFieldCollection RelatedFields = web.Lists["Authors"].GetRelatedFields();
                    foreach (SPRelatedField RelatedField in RelatedFields)
                    {
                        Console.WriteLine("Field <{0}>{5}   bound to <{1}>{5}   lookup on <{2}>{5}   SPRelationshipDeleteBehavior.{3}{5}Web <{4}>",
                            web.Lists[RelatedField.ListId].Fields[RelatedField.FieldId].InternalName,
                            web.Lists[RelatedField.ListId].Title,
                            RelatedField.LookupList,
                            RelatedField.RelationshipDeleteBehavior,
                            site.AllWebs[RelatedField.WebId].Title,
                            Environment.NewLine);
                    }
                    
                }
            }
            Console.ReadLine();
        }
    }
}
