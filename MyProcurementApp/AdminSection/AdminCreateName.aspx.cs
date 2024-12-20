using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp
{
    public partial class Default : Page
    {
        private IContainer container;
        private List<RoleModel> rolesList;
        private RoleDB roleDB;
        private int roleId;
        private bool isActive;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                await AddRoles();
                BindDropDownData();
            }
        }
        private async Task AddRoles()
        {
            container = (IContainer)Application["AutofacContainer"];
            roleDB = container.Resolve<RoleDB>();
            rolesList = await roleDB.ReadRoles("selectRoles");
        }
        
        private void BindDropDownData()
        {
            ddlRoles.DataSource = rolesList;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleID";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, new ListItem("[Enter A Role]"));
        }

        protected void OnCheckUserActive(object sender, EventArgs e)
        {
            isActive = isUserActive.Checked;
        }

        protected async void OnSubmitUser(object sender, EventArgs e)
        {
            //Calling on Container and User Database Service
            container = (IContainer)Application["AutofacContainer"];
            var userdb = container.Resolve<UserDB>();

            //Validating string with a string Extenstion Method to see if it's valid or not
            string validatedName = userName.Text.ValidateString();
            roleId = ddlRoles.SelectedValue.ValidateString().ConvertStringTo<int>();

            // Instantiating our model to hold the data
            var user = new UserModel()
            {
                Name = validatedName,
                RoleID = roleId,
                Active = isActive
            };

            //Data Should be transferred to Database
            string isCreated = await userdb.CreateUser("insertUser", user);
            isCreated.AlertSuccessORFail(this);
        }
        
    }
}