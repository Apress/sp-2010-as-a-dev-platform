using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace ItemEventReceiver.EventReceiver2
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver2 : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {
            updateItemPermissions(properties);
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            updateItemPermissions(properties);
        }

        private void updateItemPermissions(SPItemEventProperties properties)
        {
            try
            {
                //temporarely disable item firing so that we do not end in the infinite loop  42:                 
                this.EventFiringEnabled = false;
                SPListItem item = properties.ListItem;
                // now, get the same item with the elevated privileges   
                // we have to do it that way because we do not know which level of  
                // permissions the current user has  
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPList parentList = item.ParentList;
                    SPSite elevatedSite = new SPSite(parentList.ParentWeb.Site.ID);
                    SPWeb elevatedWeb = elevatedSite.OpenWeb(parentList.ParentWeb.ID);
                    SPList elevatedList = elevatedWeb.Lists[parentList.ID];
                    // get the file with the privileged permissions  
                    SPListItem elevatedItem = elevatedList.Items.GetItemById(properties.ListItem.ID);

                    // break item permissions inheritance and assign new permissions based on   
                    //Wenn keine speziellen Berechtigungen auf dieses Item gesetzt sind (Vererbung) ...
                    if (!item.HasUniqueRoleAssignments)
                        //... durchbrechen wir die Vererbungshierarchie, ohne Berechtigungen zu kopieren    
                        item.BreakRoleInheritance(false);

                    SPUser editor = elevatedWeb.EnsureUser((new SPFieldLookupValue(item["MyEditor"].ToString())).LookupValue);
                    SPUser author = elevatedWeb.EnsureUser((new SPFieldLookupValue(item["MyAuthor"].ToString())).LookupValue);
                    //Erzeugen von Rollendefinition, einmal Leser und einmal Schreibender
                    SPRoleDefinition RoleDefReader = elevatedWeb.RoleDefinitions.GetByType(SPRoleType.Reader);
                    SPRoleDefinition RoleDefWriter = elevatedWeb.RoleDefinitions.GetByType(SPRoleType.Contributor);
                    //Rollenzuweisung: Mitarbeiter bekommt Leserechte, Vorgesetzter und Personalabteilung Schreibrechte
                    SPRoleAssignment RoleAssReader = new SPRoleAssignment((SPPrincipal)editor);
                    SPRoleAssignment RoleAssWriter = new SPRoleAssignment((SPPrincipal)author);
                    //Rollenzuweisung dem ListItem hinzufügen
                    RoleAssReader.RoleDefinitionBindings.Add(RoleDefReader);
                    RoleAssWriter.RoleDefinitionBindings.Add(RoleDefWriter);
                    item.RoleAssignments.Add(RoleAssReader);
                    item.RoleAssignments.Add(RoleAssWriter);
                    item.Update();

                });

                //enable the event firing again  

                this.EventFiringEnabled = true;
            }
            catch (Exception ex)
            {

            }
        }


    }
}
