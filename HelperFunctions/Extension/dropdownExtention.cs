using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HelperFunctions.Extension
{
    public static class dropdownExtention
    {
        public static void BindDropDownData<T>(this DropDownList dropDown, string TextField, string ValueFeild, string messagae, List<T> dataSource)
        {
            dropDown.DataSource = dataSource;
            dropDown.DataTextField = TextField;
            dropDown.DataValueField = ValueFeild;
            dropDown.DataBind();
            dropDown.Items.Insert(0, new ListItem(messagae));
        }
    }
}
