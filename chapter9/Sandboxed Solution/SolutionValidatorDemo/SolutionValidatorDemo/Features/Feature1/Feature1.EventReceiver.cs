using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace APRESS.SP2010.SolutionValidatorDemo.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("34e3bb48-754f-4034-a606-650012a78aa3")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {

                SPUserCodeService userCodeService = SPUserCodeService.Local;
                if (userCodeService != null)
                {
                    userCodeService.SolutionValidators.Add(new SolutionValidator(SPUserCodeService.Local));
                    userCodeService.Update();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("SolutionValidatorDemo", ex.Message + ":" + ex.StackTrace);
            }

        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try{

          SPUserCodeService userCodeService = SPUserCodeService.Local;
            if (userCodeService!=null)
            {
               userCodeService.SolutionValidators.Remove(new Guid("481823F5-75A7-4EF8-8A4B-11C4D52D1014"));
               userCodeService.Update();
            }
            }
            catch(Exception ex)
            {
                EventLog.WriteEntry("SolutionValidatorDemo", ex.Message + ":" + ex.StackTrace);
            }
        }
      
    }
}
