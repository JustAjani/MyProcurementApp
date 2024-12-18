using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace HelperFunctions.Extension
{
    public static class stringExtension
    {
        public static string ValidateString(this string str)
        {
            string output = string.Empty;

            if (!string.IsNullOrEmpty(str)) output = str;
            else ScriptManager.RegisterStartupScript(null, typeof(Page), "showalert", "Swal.fire('Error', 'Fields should not be empty. Please try again.', 'error');", true); 

            return output;
        }

        public static decimal StringToDouble(this string str)
        {
            decimal output = 0;
            if (decimal.TryParse(str, out output)) return output;
            else return output;
        }
    }
}
