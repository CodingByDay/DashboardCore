using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebDashboardAspNetCore3.HelperMethods
{
    public static class HelperMethods
    {
            // Instead of Map Path, identical version.
            public static string MapPath(string path)
            {
                return Path.Combine(
                    (string)AppDomain.CurrentDomain.GetData("ContentRootPath"),
                    path);
            }
        
    }
}
