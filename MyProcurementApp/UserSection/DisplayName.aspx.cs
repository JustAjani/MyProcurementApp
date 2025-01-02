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
      
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            { 
                ddlUsers.BindDropDownData<UserModel>("Name", "UserID", "[Select User]", UserList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (Autofac.IContainer)Application["AutofacContainer"];
            userDB = container.Resolve<UserDB>();
            UserList = await userDB.ReadUser("selectActiveUsers");
        }

        protected void OnSelectUserChange(object sender, EventArgs e) 
        {       
            var selectedUserName = ddlUsers.SelectedItem.Text;
            var selectedID = Convert.ToInt32(ddlUsers.SelectedValue);
            Session["TransferUser"] = selectedUserName;
            Session["TransferID"] = selectedID;
            var roleID = UserList.Where(name => name.Name == selectedUserName).Select(roleid => roleid.RoleID).FirstOrDefault();
            RedirectUserOrManageHomePage(roleID, selectedUserName, selectedID);
        }
    }
}