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
        private List<UserModel> users;
        protected async Task Page_Load(object sender, EventArgs e)
        {
            await ResolveDependecies();
            if (!IsPostBack)
            {   
                BindUsers();
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            await Page_Load(this, e);
        }

        private async Task ResolveDependecies()
        {
            container = (IContainer)Application["AutofacContainer"];
            userDB = container.Resolve<UserDB>();
            users = await userDB.ReadUser("selectAllUsers");
        }

        private void BindUsers()
        {
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        protected async void OnEditCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                var btnEdit = (Button)sender;
                var row = (GridViewRow)btnEdit.NamingContainer;
                var txtName = (TextBox)row.FindControl("txtName");
                string name = txtName.Text.ValidateString();

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
            int UserID = hiddenID.Value.ConvertStringTo<int>();
            bool isActive = checkbox.Checked;

            var updatedActivity = new UserModel()
            {
                UserId = UserID,
                Active = isActive
            };

            string isUpdated = await userDB.UpdateUserActivity("updateUserActiveStatus", updatedActivity);
            isUpdated.AlertSuccessORFail(this);
        }

        protected void OnRoleChanged(object sender, EventArgs e)
        {

        }
    }
}