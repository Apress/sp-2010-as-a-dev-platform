using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace APRESS.SP2010.SolutionValidatorDemo
{
    [Guid("481823F5-75A7-4EF8-8A4B-11C4D52D1014")]
    public class SolutionValidator : SPSolutionValidator
    {
        private const string strValidatorName = "My Solution Validator";

        private byte[] LoadBytes(ReadOnlyCollection<byte> bytes)
        {
            
            byte[] fileBytes = new byte[bytes.Count];
            int count = 0;
            foreach (byte b in bytes)
            {
                fileBytes[count] = b;
                count++;
            }

            return fileBytes;
        }

        private bool CheckForFeature(string fileContents, string type)
        {
            string EorF = "<Elements";

            if (type.Equals("ReceiverAssembly"))
                EorF = "<Feature";

            if (fileContents.Contains(EorF) && fileContents.Contains("<" + type))
            {
                return true;
            }

            return false;
        }

        public SolutionValidator()
        {

        }

        public SolutionValidator(SPUserCodeService userCodeService)
            : base(strValidatorName, userCodeService)
        {
            this.Signature = 1983;
        }

        public override void ValidateAssembly(SPSolutionValidationProperties properties, SPSolutionFile assembly)
        {
            properties.ValidationErrorUrl = "/_layouts/SolutionValidatorDemo/SolutionValidationErrorPage.aspx";
            bool valid = true;
            string blockAssemblyName ="SandboxedWebPart";//"TestSolution";
            string blockPKT = "29d96910438b4111";

           // base.ValidateAssembly(properties, assembly);
           
            byte[] fileBytes = LoadBytes(assembly.OpenBinary());
            Assembly a = Assembly.Load(fileBytes);

            string[] assemblyFullName = a.FullName.ToLower().Split(',');
            string assemblyName = assemblyFullName[0];
            string version = assemblyFullName[1].Replace("version=", "").Trim();
            string culture = assemblyFullName[2].Replace("culture=", "").Trim();
            string publicKeyToken = assemblyFullName[3].Replace("publickeytoken=", "").Trim();

            //Validate AssemblyName
            if (assemblyName.Equals(blockAssemblyName.ToLower()))
            {
                valid = false;
                properties.ValidationErrorMessage += "Assembly name '" + assemblyName + "' not valid. ";
            }
            //Validate PublicKeyToken
            if (publicKeyToken.Equals(blockPKT.ToLower()))
            {
                valid = false;
                properties.ValidationErrorMessage += "Assembly PublicKeyToken '" + publicKeyToken + "' not valid. ";
            }


           

            
            if (!valid)
            {
                properties.ValidationErrorUrl += "?ErrorMessage=" + properties.ValidationErrorMessage;
            }
           
                properties.Valid = valid;
           
            
        }

        public override void ValidateSolution(SPSolutionValidationProperties properties)
        {
            properties.ValidationErrorUrl = "/_layouts/SolutionValidatorDemo/SolutionValidationErrorPage.aspx";
            bool valid = true;
            string blockSolutionID = "{3CCB9CAF-54A7-42FF-A03F-F6D6D881BC70}";
            string[] blockFileName = {"SandboxedWebPart","Test"};
            string[] blockFileExt = { "xml", "jpg","webpart" };
            string[] blockFileContent = { "Sand", "box"};
            
            
           
           ReadOnlyCollection<SPSolutionFile> files = properties.Files;
            foreach (SPSolutionFile file in files)
           {

               

                // Block Filenames
               foreach (string filename in blockFileName)
               {
                   if (file.Location.ToLower().Equals(filename))
                   {
                       valid = false;
                       properties.ValidationErrorMessage += "Filename '"+filename + "' is blocked. ";
                      
                   }
               }

               // Block FileExtensions
               foreach (string ext in blockFileExt)
               {
                   if (file.Location.ToLower().EndsWith(ext))
                   {
                       valid = false;
                       properties.ValidationErrorMessage += "File extension '" + ext + "' is blocked. ";
                      
                   }
               }

               if (file.Location.ToLower().EndsWith("xml"))
               {
                   byte[] fileBytes = LoadBytes(file.OpenBinary());

                   string fileContents = ASCIIEncoding.ASCII.GetString(fileBytes);

                   // Check for file content
                   foreach (string content in blockFileContent)
                   {
                       if (fileContents.ToLower().Contains(content))
                       {
                           valid = false;
                           properties.ValidationErrorMessage += "File '"+ file.Location + "' contains blocked characters. ";

                       }
                   }

                   // Check for Features like ContentType, CustomAction, Workflow, Receivers, ReceiverAssembly, ListTemplate, Module, Field, WebPart
                   
                       if (CheckForFeature(fileContents, "ListTemplate"))
                       {
                           valid = false;
                           properties.ValidationErrorMessage = "Solution is blocked from including List Definitions. ";
                       }
                   
               }
            

            


            }
            // Block SolutionID
            if (properties.SolutionId.Equals(new Guid(blockSolutionID)))
            {
                valid = false;
                properties.ValidationErrorMessage += "SolutionID is not valid. ";
            }

            // Block SolutionID stored in SharePoint List
            string strListName = "block";
           
            using (SPSite site = properties.Site)
                {
                    SPList list = site.OpenWeb().Lists.TryGetList(strListName);
                   if (list!=null)
                   {
                    SPListItemCollection items = list.GetItems();
                    foreach (SPListItem item in items)
                    {
                        if (properties.SolutionId.Equals(new Guid(item.Title)))
                        {
                            valid = false;
                            properties.ValidationErrorMessage += "SolutionID is not valid. ";
                            break;
                        }

                        
                    }
                   }
                }
           
           

            if (!valid)
            {
                properties.ValidationErrorUrl += "?ErrorMessage=" + properties.ValidationErrorMessage;
            }

            properties.Valid = valid;

        }


    }
}
