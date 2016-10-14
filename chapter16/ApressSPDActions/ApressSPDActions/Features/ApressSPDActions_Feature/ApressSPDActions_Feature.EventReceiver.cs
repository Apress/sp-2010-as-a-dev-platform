using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;

namespace ApressSPDActions.Features.ApressSPDActions_Feature
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("e72495ca-9fdf-4d9b-8bf4-99df050b577d")]
    public class ApressSPDActions_FeatureEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties) 
        { 
            SPWebApplication wappCurrent = (SPWebApplication)properties.Feature.Parent; 
            SPWebConfigModification modAuthorizedType = new SPWebConfigModification(); 
            modAuthorizedType.Name = "AuthType";
            modAuthorizedType.Owner = "SPDActivities"; 
            modAuthorizedType.Path = "configuration/System.Workflow.ComponentModel.WorkflowCompiler/authorizedTypes"; 
            modAuthorizedType.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            modAuthorizedType.Value = "<authorizedType Assembly=\"SPDActivities, " + "Version=1.0.0.0, Culture=neutral, PublicKeyToken=07ca925dce31cf11\" " + "Namespace=\"SPDActivities\" TypeName=\"*\" Authorized=\"True\" />"; 
            wappCurrent.WebConfigModifications.Add(modAuthorizedType); 
            wappCurrent.WebService.ApplyWebConfigModifications();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
