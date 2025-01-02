using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelperFunctions.Extension
{
    public static class ObjectExtention
    {
        public static (T, A) FindUIComponent<T, A>(this object obj , string fieldName) where T : class where A : Control
        {
             var sender =  obj as A;
             var row = sender.NamingContainer as GridViewRow;
             var component = row.FindControl(fieldName) as T;
             return (component, sender);
        }
    }
}
