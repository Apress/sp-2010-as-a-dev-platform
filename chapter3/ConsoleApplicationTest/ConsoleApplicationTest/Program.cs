using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties p;
            if (args.Length > 1 && !args[0].StartsWith("xml"))
            {
                p = Properties.ReadArguments<Properties>(args);
            }
            else
            {
                string[] param = args[0].Split(new char[] {':'}, 2);
                if (param.Length == 2 && param[0].Equals("xml"))
                {
                    if (File.Exists(param[1]))
                    {

                        XDocument doc = XDocument.Load(param[1]);
                        p = Properties.Deserialize<Properties>(doc);
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                else
                {
                    throw new ArgumentException("parameter 'xml' expected");
                }
            }
        }
    }
}
