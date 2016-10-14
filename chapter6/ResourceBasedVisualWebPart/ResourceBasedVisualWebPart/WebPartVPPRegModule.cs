using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace ResourceBasedVisualWebPart
{
    public class WebPartVPPRegModule : IHttpModule
    {
        static bool resourceProviderInitialized = false;
        static object locker = new object();

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            if (!resourceProviderInitialized)
            {
                lock (locker)
                {
                    if (!resourceProviderInitialized)
                    {
                        WebPartPathProvider wpVPP = new WebPartPathProvider(HostingEnvironment.VirtualPathProvider);                        
                        HostingEnvironment.RegisterVirtualPathProvider(wpVPP);
                        resourceProviderInitialized = true;
                    }
                }
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //
        }

        public void Dispose()
        {
            resourceProviderInitialized = false;
        }
    }
}
