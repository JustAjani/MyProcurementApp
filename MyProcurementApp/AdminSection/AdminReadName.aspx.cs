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
                gvUsers.BindDropDownFromGridView<RoleModel>("ddlRole","RoleName", "RoleID", "[Select Role]", RoleList); 
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
                (var textbox, var editbtn) = sender.FindUIComponentAndSender<TextBox, Button>("txtName");
                var name = textbox.Text.ValidateString(this);

                var updatedUser = new UserModel()
                {
                    UserId = userId,
                    Name = name
                };

                string isUpdated = await userDB.UpdateUser("updateUserName", updatedUser);
                isUpdated.AlertSuccessORFail(this);
            }
        }

        protected async void OnStatusChanged(object sender, EventArgs e)
        {
            (var hiddenID, var checkBox) = sender.FindUIComponentAndSender<HiddenField, CheckBox>("hdnUserID");
            var userID = hiddenID.Value.ValidateString(this).ConvertStringTo<int>();
            var isActive = checkBox.Checked;

            var updatedActivity = new UserModel()
            {
                UserId = userID,
                Active = isActive
            };

            string isUpdated = await userDB.UpdateUserActivity("updateUserActiveStatus", updatedActivity);
            isUpdated.AlertSuccessORFail(this);
        }

        protected async void OnRoleChanged(object sender, EventArgs e)
        {
         
            (var hiddenField, var dropDown) = sender.FindUIComponentAndSender<HiddenField, DropDownList>("hdnId4Role");
            var userID = hiddenField.Value.ValidateString(this).ConvertStringTo<int>();
            int roleID = dropDown.SelectedValue.ValidateString(this).ConvertStringTo<int>();

            var updateUserRole = new UserModel()
            {
                UserId = userID,
                RoleID = roleID,
            };

            string isUpdated = await userDB.UpdateUserRole("updateUserRole", updateUserRole);
            isUpdated.AlertSuccessORFail(this);
        }
    }
}