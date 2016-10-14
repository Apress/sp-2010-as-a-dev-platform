using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace HandleFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SPSite site = new SPSite("http://sharepointserve"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    List<SPFeatureDefinition> features = new List<SPFeatureDefinition>();
                    foreach (SPFeature feature in web.Features)
                    {
                        features.Add(feature.Definition);
                        Console.WriteLine("{0} v{1}",
                            feature.Definition.Name,
                         feature.Version);
                    }

                    //    SPFeatureDefinition feature = web.Features;
                    //web.Features.Add(feature.Id);


                    //SPFeatureDefinition feature = (SPFeatureDefinition)listBoxActivateFeatures.SelectedItem;
                    //web.Features.Remove(feature.Id);
                }
            }
            Console.ReadLine();
        }
    }
}
