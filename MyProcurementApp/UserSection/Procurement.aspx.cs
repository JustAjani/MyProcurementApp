using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp
{
    public partial class Procurement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = Session["TransferUser"].ToString();
            }
        }

        protected void btnCreateRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateProcurement.aspx");
        }

        protected void btnViewReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("DisplayProcurement.aspx");
        }
    }
}