using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelperFunctions.Extension
{
    public static class GridRowExtention
    {
        public static void BindGridData<T>(this GridView gridView, List<T> bindSourse) where T : class
        {
            gridView.DataSource = bindSourse;
            gridView.DataBind();
        }

        public static void BindDropDownFromGridView<T>(this GridView gridView, string dropdownName, string dataText, string dataValue, string message,List<T> dataSource, string tooltip = null) where T : class 
        {
            foreach(GridViewRow row in  gridView.Rows)
            {
                DropDownList ddl = (DropDownList)row.FindControl(dropdownName);
                ddl.BindDropDownData<T>(dataText, dataValue, message, dataSource, tooltip);
            }
        }

        public static void FindDataWithGrid<T>(this GridView gridView, string element, Action<T> action) where T : Control
        {
            foreach (GridViewRow row in gridView.Rows)
            {
                var control = row.FindControl(element) as T;
                if (control != null)
                {
                    action(control);
                }
            }
        }

    }
}
