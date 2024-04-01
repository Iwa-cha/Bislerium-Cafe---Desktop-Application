using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class Utils
    {
        public static string GetDesktopPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "dotnet_cw_DB");

            if(File.Exists(path))
            {
                return path;
            }
            else
            {
                Directory.CreateDirectory(path);
                return path;
            }
        }

        public static string GetItemsPath()
        {
            return Path.Combine(GetDesktopPath(), "items.json");
        }

        public static string GetOrdersPath()
        {
            return Path.Combine(GetDesktopPath(), "orders.json");
        }

        public static string GetMembersPath()
        {
            return Path.Combine(GetDesktopPath(), "members.json");
        }



        public static bool IsNumeric(string input)
        {
            Regex numericRegex = new Regex("^[0-9]+$");
            return numericRegex.IsMatch(input);
        }


    }
}
