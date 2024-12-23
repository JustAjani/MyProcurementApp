using Autofac;
using DatabaseCodeBase.DatabaseCode;
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
    public partial class AdminReadName : System.Web.UI.Page
    {
        private IContainer container;
        private UserDB userDB;
        private RoleDB roleDB;
        private List<UserModel> userList;
        private List<RoleModel> roleList;
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await ResolveDependecies();
            if (!IsPostBack)
            {
                gvUsers.BindGridData<UserModel>(userList);
                gvUsers.BindDropDownFromGridView<RoleModel>("RoleName", "RoleID", "[Select Role]", roleList); 
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        private async Task ResolveDependecies()
        {
            container = (IContainer)Application["AutofacContainer"];
            (userDB, roleDB) = container.InitializeDependency<UserDB, RoleDB>();
            roleList = await roleDB.ReadRoles("selectRoles");
            userList = await userDB.ReadUser("selectAllUsers");
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                var btnEdit = (Button)sender;
                var row = (GridViewRow)btnEdit.NamingContainer;
                var txtName = (TextBox)row.FindControl("txtName");
                string name = txtName.Text.ValidateString(this);

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
            var checkbox = (CheckBox)sender;
            var row = (GridViewRow)checkbox.NamingContainer;
            var hiddenID = (HiddenField)row.FindControl("hdnUserID");
            int UserID = hiddenID.Value.ValidateString(this).ConvertStringTo<int>();
            bool isActive = checkbox.Checked;

            var updatedActivity = new UserModel()
            {
                UserId = UserID,
                Active = isActive
            };

            string isUpdated = await userDB.UpdateUserActivity("updateUserActiveStatus", updatedActivity);
            isUpdated.AlertSuccessORFail(this);
        }

        protected async void OnRoleChanged(object sender, EventArgs e)
        {
            var dropdown = (DropDownList)sender;
            var row = (GridViewRow)dropdown.NamingContainer;
            var hiddenID = (HiddenField)row.FindControl("hdnId4Role");
            int UserID = hiddenID.Value.ValidateString(this).ConvertStringTo<int>();
            int RoleID = dropdown.SelectedValue.ValidateString(this).ConvertStringTo<int>();

            var updateUserRole = new UserModel()
            {
                UserId = UserID,
                RoleID = RoleID,
            };

            string isUpdated = await userDB.UpdateUserRole("updateUserRole", updateUserRole);
            isUpdated.AlertSuccessORFail(this);
        }
    }
}