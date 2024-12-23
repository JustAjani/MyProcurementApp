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
        public static string ValidateString(this string str, Page page)
        {
            string output = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(str)) output = str;
                else throw new Exception("Fields should not be empty. Please try again.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
                

            return output;
        }

        public static T ConvertStringTo<T>(this string str) where T : struct, IConvertible
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }

        public static void AlertSuccessORFail(this string str, Page page)
        {
            if (str.Contains("Successfull") || str.Contains("SuccessFully") || str.Contains("SuccesFul!"))
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", $"Swal.fire('Success', '{str}', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", $"Swal.fire('Error', '{str}', 'error');", true);
            }
        }
        
        public static DateTime StringToDate(this string str, Page page)
        {
            DateTime output = DateTime.MinValue;
            try
            {
                if (DateTime.TryParse(str, out var value)) output = value.Date;
                else throw new Exception($"{str} is not a valid Date");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
            return output;
        }
    }
}
