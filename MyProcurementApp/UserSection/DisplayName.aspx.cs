using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autofac;
using DatabaseCodeBase.DatabaseCode;
using System.Threading.Tasks;
using HelperFunctions.Extension;
using DatabaseCodeBase.ServiceInterface;

namespace MyProcurementApp
{
    public partial class DisplayName : System.Web.UI.Page
    {
        protected List<UserModel> users;
        private Autofac.IContainer container;
        private UserDB userDB;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await AddUsersToList();
                BindUsersToDropDown();
            }
        }

        private async Task AddUsersToList()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            userDB = container.Resolve<UserDB>();
            //Stores the users in list
            users = await userDB.ReadUser("selectActiveUsers");

        }

        private void BindUsersToDropDown()
        {
            //Uses users List as Data Source then Binds data to the drop down
            ddlUsers.DataSource = users;
            ddlUsers.DataTextField = "Name";
            ddlUsers.DataBind();
            ddlUsers.Items.Insert(0, new ListItem("[Select User]"));
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e) 
        {       
            var selectedUserName = ddlUsers.SelectedValue;
            var id = ddlUsers.SelectedIndex;
            //Stores name in a session
            Session["TransferUser"] = selectedUserName; 
            //Redirects to a new Page where the User will be greated
            Response.Redirect($"Procurement.aspx?userName={selectedUserName}?id={id}"); 
        }
    }
}