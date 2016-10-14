using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;
using APRESS.SP2010.FullTrustProxy;

namespace APRESS.SP2010.DeployFullTrustProxy.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("65a428e1-c14a-4f9c-b7fe-9dbac8e830d4")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPUserCodeService userCodeService = SPUserCodeService.Local;

                if (userCodeService != null)
                {

                    SPProxyOperationType operation =
                        new SPProxyOperationType(
                            new FullTrustProxyArgs().FullTrustProxyOpsAssemblyName,
                            new FullTrustProxyArgs().FullTrustProxyOpsTypeName);

                    userCodeService.ProxyOperationTypes.Add(operation);
                    userCodeService.Update();


                }
                else
                {

                }
            }
            catch 
            {
              // Exceptionhandling
            }

        }


        // handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPUserCodeService userCodeService = SPUserCodeService.Local;

                if (userCodeService != null)
                {
                    SPProxyOperationType operation = null;
                    foreach (SPProxyOperationType operationType in userCodeService.ProxyOperationTypes)
                    {
                        if (operationType.AssemblyName.Equals(new FullTrustProxyArgs().FullTrustProxyOpsAssemblyName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            operation = operationType;
                            break;
                        }
                    }

                    if (operation != null)
                    {
                        userCodeService.ProxyOperationTypes.Remove(operation);
                        userCodeService.Update();
                    }



                }
                else
                {

                }
            }
            catch 
            {
                // Exceptionhandling
            }
        }


       
    }
}
