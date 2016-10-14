using System;
using System.IO;
using System.Security;
using Microsoft.SharePoint.UserCode;

[assembly: AllowPartiallyTrustedCallers()]
namespace APRESS.SP2010.FullTrustProxy
{
    public class FullTrustProxyOps : SPProxyOperation
    {
        public override object Execute(SPProxyOperationArgs args)
        {
            if (args != null)
            {
                string tempPath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Machine);

                FullTrustProxyArgs fileArgs = args as FullTrustProxyArgs;
                FileStream fStream =
                    new FileStream(tempPath + @"\SPFullTrustProxyLog.txt",
                       FileMode.Append);
                fStream.Write(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(fileArgs.FileContents), 0,
                 fileArgs.FileContents.Length);
                fStream.Flush();
                fStream.Close();
                return fileArgs.FileContents;
            }
            else return null;
        }
    }
}
