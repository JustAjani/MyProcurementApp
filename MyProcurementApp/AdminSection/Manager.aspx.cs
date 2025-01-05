using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = Session["TransferUser"].ToString();
            }
        }

        protected void RedirectToManageUsers(object sender, EventArgs e)
        {
            Response.Redirect("AdminReadName.aspx");
        }

        protected void RedirectToCreateUser(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreateName.aspx");
        }

        protected void RedirectToCreateProcurement(object sender, EventArgs e)
        {
            Response.Redirect("~/UserSection/CreateProcurement.aspx");
        }

        protected void RedirectToViewProcurements(object sender, EventArgs e)
        {
            Response.Redirect("AdminReadUserProcurement.aspx");
        }

        protected void RedirectToManageTypes(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreateProcurement.aspx");
        }

        protected void RedirectToViewTypes(object sender, EventArgs e)
        {
            Response.Redirect("AdminReadProcurement.aspx");
        }

        protected void RedirectToCreateCC(object sender, EventArgs e)
        {
            Response.Redirect("AdminCreateCostCenter.aspx");
        }

        protected void RedirectToManageCostCenters(object sender, EventArgs e)
        {
            Response.Redirect("AdminReadCostCentre.aspx");
        }

    }
}