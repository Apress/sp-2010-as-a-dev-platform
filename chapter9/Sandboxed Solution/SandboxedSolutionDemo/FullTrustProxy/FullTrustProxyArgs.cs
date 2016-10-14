using System;
using System.Reflection;
using Microsoft.SharePoint.UserCode;

namespace APRESS.SP2010.FullTrustProxy
{
    [Serializable]
    public class FullTrustProxyArgs : SPProxyOperationArgs
    {
        public string FileContents { get; set; }

        public string FullTrustProxyOpsAssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().FullName; //"FullTrustProxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=29d96910438b4111";
            }
        }
        public string FullTrustProxyOpsTypeName
        {
            get
            {
               return "APRESS.SP2010.FullTrustProxy.FullTrustProxyOps";
            }
        }

        public FullTrustProxyArgs()
        {

        }
        public FullTrustProxyArgs(string fileContents)
        {
            this.FileContents = fileContents;
        }
    }
}