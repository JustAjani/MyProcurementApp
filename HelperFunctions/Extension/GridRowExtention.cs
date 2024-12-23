using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HelperFunctions.Extension
{
    public static class GridRowExtention
    {
        public static void BindGridData<T>(this GridView gridView, List<T> bindSourse)
        {
            gridView.DataSource = bindSourse;
            gridView.DataBind();
        }

        public static void BindDropDownFromGridView<T>(this GridView gridView, string dataText, string dataValue, string message ,List<T> dataSource)
        {
            foreach(GridViewRow row in  gridView.Rows)
            {
                DropDownList ddl = (DropDownList)row.FindControl("ddlRole");
                ddl.BindDropDownData<T>(dataText, dataValue, message, dataSource);
            }
        }
    }
}
