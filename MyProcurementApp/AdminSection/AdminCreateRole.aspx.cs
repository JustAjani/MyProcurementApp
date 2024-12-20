using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminCreateRole : System.Web.UI.Page
    {
        private IContainer container;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void OnRoleSubmit(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            var roleDB = container.Resolve<RoleDB>();

            string userRole = Role.Text.ValidateString();

            var RoleToDb = new RoleModel()
            {
                RoleName = userRole
            };

            string isRoleCreated = await roleDB.CreateRoles("insertRole", RoleToDb); 
            if (isRoleCreated.Contains("Successfully"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Success', '{isRoleCreated}', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Error', '{isRoleCreated}', 'error');", true);
            }
        }
    }
}