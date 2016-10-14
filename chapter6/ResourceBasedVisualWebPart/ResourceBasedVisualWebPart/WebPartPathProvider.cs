using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Web.Caching;

namespace ResourceBasedVisualWebPart
{
    public class WebPartPathProvider : System.Web.Hosting.VirtualPathProvider
    {
        private VirtualPathProvider parent;

        public WebPartPathProvider(VirtualPathProvider parent) 
        {
            this.parent = parent;
        }
        
        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.Contains("VisualWebPart");
        }

        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) || parent.FileExists(virtualPath));
        }
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
                return new AssemblyResourceVirtualFile(virtualPath);
            else
                return parent.GetFile(virtualPath);
        }
        public override System.Web.Caching.CacheDependency
               GetCacheDependency(string virtualPath,
               System.Collections.IEnumerable virtualPathDependencies,
               DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                
                return null;
            }
            else
                return parent.GetCacheDependency(virtualPath,
                       virtualPathDependencies, utcStart);
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return parent.CombineVirtualPaths(basePath, relativePath);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return parent.DirectoryExists(virtualDir);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {            
            return parent.GetDirectory(virtualDir);
        }

        public override string GetCacheKey(string virtualPath)
        {
            if (!IsAppResourcePath(virtualPath))
                return parent.GetCacheKey(virtualPath);
            else
                return null; // base.GetCacheKey(virtualPath);
        }

        public override string GetFileHash(string virtualPath, System.Collections.IEnumerable virtualPathDependencies)
        {
            if (!IsAppResourcePath(virtualPath)) 
                return parent.GetFileHash(virtualPath, virtualPathDependencies);
            else
                return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override object InitializeLifetimeService()
        {
            return parent.InitializeLifetimeService();
        }

    }

    class AssemblyResourceVirtualFile : VirtualFile
    {
        string path;
        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }
        public override System.IO.Stream Open()
        {
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];
            assemblyName = Path.Combine(HttpRuntime.BinDirectory,
                                        assemblyName);
            System.Reflection.Assembly assembly =
               System.Reflection.Assembly.LoadFile(assemblyName);
            if (assembly != null)
            {
                return assembly.GetManifestResourceStream(resourceName);
            }
            return null;
        }




    }
}
