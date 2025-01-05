using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminReadName : PageUtil
    {
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            {
                gvUsers.BindGridData<UserModel>(UserList);
                gvUsers.BindDropDownFromGridView<RoleModel>("ddlRole", "RoleName", "RoleID", "[Select Role]", RoleList);
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            (userDB, roleDB) = container.InitializeDependency<UserDB, RoleDB>();
            RoleList = await roleDB.ReadRoles("selectRoles");
            UserList = await userDB.ReadUser("selectAllUsers");
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                (var name, _) = sender.FindUIComponent<TextBox, Button>("txtName");
                (var isActive, _) = sender.FindUIComponent<CheckBox, Button>("chkActive");
                (var roleChange, _) = sender.FindUIComponent<DropDownList, Button>("ddlRole");

                var RoleID = 0;  
                var roleIDString = roleChange.SelectedValue.ValidateString(this);

                if (int.TryParse(roleIDString, out int value)) RoleID = value;
                else RoleID = UserList.Where(s => s.Name == name.Text).Select(s => s.RoleID).FirstOrDefault();

                var updatedUser = new UserModel()
                {
                    UserId = userId,
                    Name = name.Text.ValidateString(this),
                    RoleID = RoleID,
                    Active = isActive.Checked
                };

                string isUpdated = await userDB.UpdateUser("updateUserName", updatedUser);
                isUpdated.AlertSuccessORFail(this);
            }
        }
    }
}