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
using DatabaseCodeBase.DBPageUtil;

namespace MyProcurementApp
{
    public partial class DisplayName : PageUtil
    {
     
        
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await InitializeDependecy();
                ddlUsers.BindDropDownData<UserModel>("Name", "UserID", "[Select User]", UserList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            userDB = container.Resolve<UserDB>();
            //Stores the users in list
            UserList = await userDB.ReadUser("selectActiveUsers");
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e) 
        {       
            var selectedUserName = ddlUsers.SelectedItem;
            var id = ddlUsers.SelectedValue;
            //Stores name in a session
            Session["TransferUser"] = selectedUserName;
            Session["TransferID"] = id;
            //Redirects to a new Page where the User will be greated
            Response.Redirect($"Procurement.aspx?userName={selectedUserName}?id={id}"); 
        }
    }
}