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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp
{
    public partial class Default : PageUtil
    {
        private bool isActive;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                await InitializeDependecy();
                ddlRoles.BindDropDownData<RoleModel>("RoleName", "RoleID", "[Enter A Role]", roleList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            roleDB = container.Resolve<RoleDB>();
            roleList = await roleDB.ReadRoles("selectRoles");
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

            //Validating string with a string Extenstion Method to see if it's valid or not then convert to any number data type
            var validatedName = userName.Text.ValidateString(this);
            var roleId = ddlRoles.SelectedValue.ValidateString(this).ConvertStringTo<int>();

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