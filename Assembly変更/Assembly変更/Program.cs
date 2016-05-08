using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Configuration;
using System.Collections.Specialized;
namespace AssemblyChange
{
    public class Program
    {
        static void Main(string[] args)
        {



        }

        public static void ReadConfig()
        {
            // Read all the keys from the config file
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (string s in sAll.AllKeys)
                Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));
            Console.ReadLine();
        }
    }
}
