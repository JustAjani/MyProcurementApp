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

        protected async void Page_Load(object sender, EventArgs e)
        {
            await InitializeDependecy();
            if (!IsPostBack)
            {
                ddlRoles.BindDropDownData<RoleModel>("RoleName", "RoleID", "[Enter A Role]", RoleList);
            }
        }

        protected override async Task InitializeDependecy()
        {
            container = (IContainer)Application["AutofacContainer"];
            roleDB = container.Resolve<RoleDB>();
            userDB = container.Resolve<UserDB>();
            RoleList = await roleDB.ReadRoles("selectRoles");
        }

        protected async void OnSubmitUser(object sender, EventArgs e)
        {
            var validatedName = userName.Text.ValidateString(this);
            var roleId = ddlRoles.SelectedValue.ValidateString(this).ConvertStringTo<int>();

            var user = new UserModel()
            {
                Name = validatedName,
                RoleID = roleId,
                Active = true,
            };

            string isCreated = await userDB.CreateUser("insertUser", user);
            isCreated.AlertSuccessORFail(this);
        }
        
    }
}